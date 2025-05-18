import { Button } from "@mui/material";
import DataTable, { Column } from "../../../components/common/DataTable";
import { useCallback, useEffect, useState } from "react";
import { ApiResponse, DefaultParams } from "../../../types/api";
import { useQuery } from "@tanstack/react-query";
import { Transaction } from "../../../types/invoice";
import { getTransactions } from "../../../api/transaction";
import TransactionDetailDialog from "../../../components/transactions/TransactionDetailDialog";


const columns: Column<Transaction>[] = [
    { key: 'buyerId', label: 'خریدار' },
    { key: 'dateTime', label: 'تاریخ' },
    { key: 'price', label: 'قیمت', render: (value) => `${value.toLocaleString()} تومان` },
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
