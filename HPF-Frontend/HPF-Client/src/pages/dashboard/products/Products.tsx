import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import DataTable, { Column } from '../../../components/common/DataTable';
import { deleteProduct, getProducts } from '../../../api/product';
import { Product } from '../../../types/product';
import { ApiResponse, DefaultParams } from '../../../types/api';
import { Button } from '@mui/material';
import { useState, useEffect, useCallback } from 'react';
import DeleteConfirmDialog from '../../../components/common/DeleteConfirmDialog';
import { useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';

const columns: Column<Product>[] = [
  { key: 'name', label: 'نام محصول' },
  { key: 'quantity', label: 'تعداد' },
  { key: 'price', label: 'قیمت', render: (value) => `${value.toLocaleString()} تومان` },
];

export default function ProductsTable() {
  const navigate = useNavigate(); // Initialize useRouter to handle navigation

  const [queryParams, setQueryParams] = useState<DefaultParams>({
    pageSize: 10,
    pageIndex: 0,
    search: ''
  });

  const { refetch } = useQuery<ApiResponse<Product[]>, Error>({
    queryKey: ['getProducts', queryParams],
    queryFn: () => getProducts(queryParams),
    enabled: false
  });

  const [refreshKey, setRefreshKey] = useState(0);

  const queryClient = useQueryClient(); // To access query client for invalidating the query

  const { mutate: deleteProductMutation } = useMutation({
    mutationFn: deleteProduct,
    onSuccess: () => {
      // Handle success
      queryClient.invalidateQueries({ queryKey: ['getProducts', queryParams] })
      toast.success('عملیات با موفقیت انجام شد.')

    },
    onError: (error) => {
      toast.error(error.message)
    },
  });

  const [openDialog, setOpenDialog] = useState(false);
  const [selectedRow, setSelectedRow] = useState<Product | null>(null);


  useEffect(() => {
    refetch();
  }, [queryParams, refetch]);

  const onDelete = useCallback((row: Product) => {
    setSelectedRow(row);
    setOpenDialog(true);
  }, []);

  const confirmDelete = useCallback(async () => {
    if (!selectedRow) return;

    deleteProductMutation(selectedRow.id)
    // Close the dialog

    setOpenDialog(false);
    setSelectedRow(null)
    setRefreshKey(prev => prev + 1);
  }, [selectedRow]);

  const onEdit = useCallback((row: Product) => {
    navigate(`/dashboard/product/${row.id}`);
  }, []);

  const fetchData = useCallback(async (parameters?: DefaultParams): Promise<ApiResponse<Product[]>> => {
    const params: DefaultParams = {
      pageSize: parameters?.pageSize || 10,
      pageIndex: parameters?.pageIndex || 0,
      search: parameters?.search || ''
    };
    const response = await getProducts(params);
    return response;
  }, []);

  return (
    <>
      <DataTable<Product>
        columns={columns}
        fetchData={fetchData} // Pass the fetchData function to the DataTable
        refetch={refetch}
        defaultRowsPerPage={10}
        reloadKey={refreshKey} // <- NEW
        renderActions={(row) => (
          <>
            <Button color="primary" size="small" onClick={() => onEdit(row)}>ویرایش</Button>
            <Button color="error" size="small" onClick={() => onDelete(row)}>حذف</Button>
          </>
        )}
      />

      <DeleteConfirmDialog
        open={openDialog}
        onClose={() => setOpenDialog(false)}
        onConfirm={confirmDelete}
      />
    </>
  );
}
