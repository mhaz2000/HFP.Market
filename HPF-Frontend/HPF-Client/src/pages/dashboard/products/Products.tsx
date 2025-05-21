import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import DataTable, { Column } from '../../../components/common/DataTable';
import { deleteProduct, getProducts } from '../../../api/product';
import { Product } from '../../../types/product';
import { ApiResponse, DefaultParams } from '../../../types/api';
import { Button } from '@mui/material';
import { useState, useCallback } from 'react';
import DeleteConfirmDialog from '../../../components/common/DeleteConfirmDialog';
import { useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';
import { toPersianNumber } from '../../../lib/PersianNumberConverter';

const columns: Column<Product>[] = [
  { key: 'name', label: 'نام محصول' },
  { key: 'code', label: 'کد محصول' },
  { key: 'quantity', label: 'تعداد', render: (value) => toPersianNumber(value) },
  { key: 'price', label: 'قیمت', render: (value) => `${toPersianNumber(value.toLocaleString())} تومان` },
  { key: 'purchasePrice', label: 'قیمت خرید', render: (value) => `${toPersianNumber(value.toLocaleString())} تومان` },
  { key: 'profit', label: 'سود حاصل از فروش', render: (value) => `${toPersianNumber(value.toLocaleString())} تومان` },
];

export default function ProductsTable() {
  const navigate = useNavigate(); // Initialize useRouter to handle navigation
  const [refreshKey, setRefreshKey] = useState(0);
  const [openDialog, setOpenDialog] = useState(false);
  const [selectedRow, setSelectedRow] = useState<Product | null>(null);
  
  const [queryParams] = useState<DefaultParams>({
    pageSize: 10,
    pageIndex: 0,
    search: ''
  });

  const { refetch } = useQuery<ApiResponse<Product[]>, Error>({
    queryKey: ['getProducts', queryParams],
    queryFn: () => getProducts(queryParams),
    enabled: false
  });


  const queryClient = useQueryClient(); // To access query client for invalidating the query

  
    const { mutate: deleteProductMutation } = useMutation({
    mutationFn: deleteProduct,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['getProducts', queryParams] });
      setRefreshKey(prev => prev + 1)
      toast.success('عملیات با موفقیت انجام شد.');
    },
    onError: (error) => {
      toast.error(error.message);
    },
  });
  
  const confirmDelete = useCallback(() => {
    if (!selectedRow) return;
    
    deleteProductMutation(selectedRow.id);
    setOpenDialog(false);
    setSelectedRow(null);
  }, [selectedRow, deleteProductMutation]);

  const onDelete = useCallback((row: Product) => {
    setSelectedRow(row);
    setOpenDialog(true);
  }, []);

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
