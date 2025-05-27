import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import DataTable, { Column } from '../../../components/common/DataTable';
import { ApiResponse, DefaultParams } from '../../../types/api';
import { Button } from '@mui/material';
import { useState, useCallback } from 'react';
import DeleteConfirmDialog from '../../../components/common/DeleteConfirmDialog';
import { useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';
import { toPersianNumber } from '../../../lib/PersianNumberConverter';
import { PurchaseInvoiceTable } from '../../../types/purchaseInvoice';
import { deletePurchaseInvoice, getPurchaseInvoices } from '../../../api/purchaseInvoice';


const columns: Column<PurchaseInvoiceTable>[] = [
    { key: 'date', label: 'تاریخ فاکتور' },
    { key: 'price', label: 'مبلغ کل فاکتور', render: (value) => `${toPersianNumber(value.toLocaleString())} تومان` },
];

export default function PurchaseInvoices() {
    const navigate = useNavigate(); // Initialize useRouter to handle navigation
    const [refreshKey, setRefreshKey] = useState(0);
    const [openDialog, setOpenDialog] = useState(false);
    const [selectedRow, setSelectedRow] = useState<PurchaseInvoiceTable | null>(null);

    const [queryParams] = useState<DefaultParams>({
        pageSize: 10,
        pageIndex: 0,
        search: ''
    });

    const { refetch } = useQuery<ApiResponse<PurchaseInvoiceTable[]>, Error>({
        queryKey: ['getPurchaseInvoices', queryParams],
        queryFn: () => getPurchaseInvoices(queryParams),
        enabled: false
    });


    const queryClient = useQueryClient(); // To access query client for invalidating the query


    const { mutate: deletePurchaseMutation } = useMutation({
        mutationFn: deletePurchaseInvoice,
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['getPurchaseInvoices', queryParams] });
            setRefreshKey(prev => prev + 1)
            toast.success('عملیات با موفقیت انجام شد.');
        },
        onError: (error) => {
            toast.error(error.message);
        },
    });

    const confirmDelete = useCallback(() => {
        if (!selectedRow) return;

        deletePurchaseMutation(selectedRow.id);
        setOpenDialog(false);
        setSelectedRow(null);
    }, [selectedRow, deletePurchaseMutation]);

    const onDelete = useCallback((row: PurchaseInvoiceTable) => {
        setSelectedRow(row);
        setOpenDialog(true);
    }, []);

    const onEdit = useCallback((row: PurchaseInvoiceTable) => {
        navigate(`/dashboard/edit-purchase-invoice/${row.id}`);
    }, []);

    const fetchData = useCallback(async (parameters?: DefaultParams): Promise<ApiResponse<PurchaseInvoiceTable[]>> => {
        const params: DefaultParams = {
            pageSize: parameters?.pageSize || 10,
            pageIndex: parameters?.pageIndex || 0,
            search: parameters?.search || ''
        };
        const response = await getPurchaseInvoices(params);
        return response;
    }, []);

    return (
        <>
            
            <DataTable<PurchaseInvoiceTable>
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
