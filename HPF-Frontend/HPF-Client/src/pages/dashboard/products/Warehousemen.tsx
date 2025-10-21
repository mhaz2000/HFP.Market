import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import DataTable, { Column } from '../../../components/common/DataTable';
import { ApiResponse, DefaultParams } from '../../../types/api';
import { Button } from '@mui/material';
import { useState, useCallback } from 'react';
import DeleteConfirmDialog from '../../../components/common/DeleteConfirmDialog';
import { toast } from 'react-toastify';
import { toPersianNumber } from '../../../lib/PersianNumberConverter';
import { type Warehosueman } from '../../../types/warehouseman';
import { deleteWarehouseman, getWarehousemen } from '../../../api/warehouseman';
import NewWarehousemanDialog from '../../../components/dashboards/waresalemen/NewWarehousemanDialog';


const columns: Column<Warehosueman>[] = [
  { key: 'name', label: 'نام انباردار' },
  { key: 'uId', label: 'شناسه', render: (value) => toPersianNumber(value) },
];

export default function Warehosuemen() {
  const [openAddDialog, setOpenAddDialog] = useState(false); // NEW

  const [refreshKey, setRefreshKey] = useState(0);
  const [openDialog, setOpenDialog] = useState(false);
  const [selectedRow, setSelectedRow] = useState<Warehosueman | null>(null);

  const [queryParams] = useState<DefaultParams>({
    pageSize: 10,
    pageIndex: 0,
    search: ''
  });

  const { refetch } = useQuery<ApiResponse<Warehosueman[]>, Error>({
    queryKey: ['getWarehousemen', queryParams],
    queryFn: () => getWarehousemen(queryParams),
    enabled: false
  });


  const queryClient = useQueryClient(); // To access query client for invalidating the query


  const { mutate: deleteWarehousemanMutation } = useMutation({
    mutationFn: deleteWarehouseman,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['getWarehousemen', queryParams] });
      setRefreshKey(prev => prev + 1)
      toast.success('عملیات با موفقیت انجام شد.');
    },
    onError: (error) => {
      toast.error(error.message);
    },
  });

  const confirmDelete = useCallback(() => {
    if (!selectedRow) return;

    deleteWarehousemanMutation(selectedRow.id);
    setOpenDialog(false);
    setSelectedRow(null);
  }, [selectedRow, deleteWarehousemanMutation]);

  const onDelete = useCallback((row: Warehosueman) => {
    setSelectedRow(row);
    setOpenDialog(true);
  }, []);


  const fetchData = useCallback(async (parameters?: DefaultParams): Promise<ApiResponse<Warehosueman[]>> => {
    const params: DefaultParams = {
      pageSize: parameters?.pageSize || 10,
      pageIndex: parameters?.pageIndex || 0,
      search: parameters?.search || ''
    };
    const response = await getWarehousemen(params);
    return response;
  }, []);

  return (
    <>
      <Button
        variant="contained"
        color="primary"
        sx={{ mb: 2 }}
        onClick={() => setOpenAddDialog(true)}
      >
        افزودن انباردار
      </Button>
      <DataTable<Warehosueman>
        columns={columns}
        fetchData={fetchData} // Pass the fetchData function to the DataTable
        refetch={refetch}
        defaultRowsPerPage={10}
        reloadKey={refreshKey} // <- NEW
        renderActions={(row) => (
          <>
            <Button color="error" size="small" onClick={() => onDelete(row)}>حذف</Button>
          </>
        )}
      />

      <DeleteConfirmDialog
        open={openDialog}
        onClose={() => setOpenDialog(false)}
        onConfirm={confirmDelete}
      />

      <NewWarehousemanDialog
        open={openAddDialog}
        onClose={() => {
          setOpenAddDialog(false)
          setRefreshKey(prev => prev + 1)
        }}
        onSuccess={refetch} // refresh table after creation
      />
    </>
  );
}
