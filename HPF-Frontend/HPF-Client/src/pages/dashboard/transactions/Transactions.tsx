import { Button } from "@mui/material";
import DataTable, { Column } from "../../../components/common/DataTable";
import { useCallback, useEffect, useState } from "react";
import { ApiResponse, DefaultParams } from "../../../types/api";
import { useQuery } from "@tanstack/react-query";
import { Transaction } from "../../../types/invoice";
import { downloadTransactionsExcel, getTransactions } from "../../../api/transaction";
import TransactionDetailDialog from "../../../components/transactions/TransactionDetailDialog";
import DownloadIcon from '@mui/icons-material/Download';
import { toPersianNumber } from "../../../lib/PersianNumberConverter";


const columns: Column<Transaction>[] = [
    { key: 'buyerId', label: 'خریدار', render: (value) => `${toPersianNumber(value)}` },
    { key: 'dateTime', label: 'تاریخ', render: (value) => `${toPersianNumber(value)}` },
    { key: 'price', label: 'قیمت', render: (value) => `${toPersianNumber(value.toLocaleString())} تومان` },
];

export default function Transactions() {


    const [queryParams] = useState<DefaultParams>({
        pageSize: 10,
        pageIndex: 0,
        search: ''
    });

    const { refetch } = useQuery<ApiResponse<Transaction[]>, Error>({
        queryKey: ['getProducts', queryParams],
        queryFn: () => getTransactions(queryParams),
        enabled: false
    });

    const [dialogOpen, setDialogOpen] = useState(false);
    const [transactionId, setTransactionId] = useState<string | null>(null);
    useEffect(() => {
        refetch();
    }, [queryParams, refetch]);

    const onDetail = useCallback((row: Transaction) => {
        setDialogOpen(true)
        setTransactionId(row.transactionId)
    }, []);

    const fetchData = useCallback(async (parameters?: DefaultParams): Promise<ApiResponse<Transaction[]>> => {
        const params: DefaultParams = {
            pageSize: parameters?.pageSize || 10,
            pageIndex: parameters?.pageIndex || 0,
            search: parameters?.search || ''
        };
        const response = await getTransactions(params);
        return response;
    }, []);

    return (
        <>
            <Button
                variant="contained"
                color="primary"
                startIcon={<DownloadIcon />}
                onClick={() => downloadTransactionsExcel()}
                sx={{
                    mb: 2,
                    fontFamily: 'inherit',
                    fontSize: '16px',
                    borderRadius: '12px',
                    padding: '8px 20px',
                    backgroundColor: '#1976d2',
                    boxShadow: '0 3px 5px rgba(0,0,0,0.2)',
                    ':hover': {
                        backgroundColor: '#115293',
                    },
                    direction: 'rtl'
                }}
            >
                دانلود همه تراکنش‌ها
            </Button>
            <DataTable<Transaction>
                columns={columns}
                fetchData={fetchData} // Pass the fetchData function to the DataTable
                refetch={refetch}
                defaultRowsPerPage={10}
                renderActions={(row) => (
                    <>
                        <Button color="info" size="small" onClick={() => onDetail(row)}>جزئیات</Button>
                    </>
                )}
            />

            <TransactionDetailDialog
                open={dialogOpen}
                transactionId={transactionId as string}
                onClose={() => setDialogOpen(false)}
            />

        </>
    )
}
