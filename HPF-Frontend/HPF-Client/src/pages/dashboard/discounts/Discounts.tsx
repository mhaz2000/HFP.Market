import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import DataTable, { Column } from '../../../components/common/DataTable';
import { ApiResponse, DefaultParams } from '../../../types/api';
import { Button } from '@mui/material';
import { useState, useCallback } from 'react';
import DeleteConfirmDialog from '../../../components/common/DeleteConfirmDialog';
// import { useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';
import { Discount } from '../../../types/discount';
import { deleteDiscount, getDiscounts } from '../../../api/discount';
import { toPersianNumber } from '../../../lib/PersianNumberConverter';

const columns: Column<Discount>[] = [
  { key: 'name', label: 'نام محصول' },
  { key: 'code', label: 'کد محصول' },
  { key: 'percentage', label: 'درصد', render: (value) => toPersianNumber(value) },
  { key: 'startDate', label: 'تاریخ شروع اعتبار', render: (value) => toPersianNumber(value) },
  { key: 'endDate', label: 'تاریخ پایان اعتبار',  render: (value) => toPersianNumber(value)},
  { key: 'usageLimitPerUser', label: 'تعداد مجاز استفاده', render: (value) => toPersianNumber(value) },
  { key: 'maxAmount', label: 'سقف مبلغ تخفیف', render: (value) => `${toPersianNumber(value.toLocaleString())} تومان` },
];

export default function DiscountsTable() {
  // const navigate = useNavigate(); // Initialize useRouter to handle navigation
  const [openDialog, setOpenDialog] = useState(false);
  const [selectedRow, setSelectedRow] = useState<Discount | null>(null);

  const [queryParams] = useState<DefaultParams>({
    pageSize: 10,
    pageIndex: 0,
    search: ''
  });

  const { refetch } = useQuery<ApiResponse<Discount[]>, Error>({
    queryKey: ['getDiscounts', queryParams],
    queryFn: () => getDiscounts(queryParams),
    enabled: false
  });

  const [refreshKey, setRefreshKey] = useState(0);

  const queryClient = useQueryClient(); // To access query client for invalidating the query

  const { mutate: deleteDiscountMutation } = useMutation({
  mutationFn: deleteDiscount,
  onSuccess: () => {
    queryClient.invalidateQueries({ queryKey: ['getDiscounts', queryParams] });
    setRefreshKey(prev => prev + 1)
    toast.success('عملیات با موفقیت انجام شد.');
  },
  onError: (error) => {
    toast.error(error.message);
  },
});

const confirmDelete = useCallback(() => {
  if (!selectedRow) return;
  
  deleteDiscountMutation(selectedRow.id);
  setOpenDialog(false);
  setSelectedRow(null);
}, [selectedRow, deleteDiscountMutation]);


  const onDelete = useCallback((row: Discount) => {
    setSelectedRow(row);
    setOpenDialog(true);
  }, []);


  // const onEdit = useCallback((row: Discount) => {
  //   navigate(`/dashboard/discount/${row.id}`);
  // }, []);

  const fetchData = useCallback(async (parameters?: DefaultParams): Promise<ApiResponse<Discount[]>> => {
    const params: DefaultParams = {
      pageSize: parameters?.pageSize || 10,
      pageIndex: parameters?.pageIndex || 0,
      search: parameters?.search || ''
    };
    const response = await getDiscounts(params);
    return response;
  }, []);

  return (
    <>
      <DataTable<Discount>
        columns={columns}
        fetchData={fetchData} // Pass the fetchData function to the DataTable
        refetch={refetch}
        defaultRowsPerPage={10}
        reloadKey={refreshKey} // <- NEW
        renderActions={(row) => (
          <>
            {/* <Button color="primary" size="small" onClick={() => onEdit(row)}>ویرایش</Button> */}
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
