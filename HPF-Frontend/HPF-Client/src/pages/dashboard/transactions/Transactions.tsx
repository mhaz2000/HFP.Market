import { Box, Button, FormControl } from "@mui/material";
import DataTable, { Column } from "../../../components/common/DataTable";
import { useCallback, useEffect, useState } from "react";
import { ApiResponse } from "../../../types/api";
import { useQuery } from "@tanstack/react-query";
import { Transaction, TransactionFilter } from "../../../types/invoice";
import { downloadTransactionsExcel, getTransactions } from "../../../api/transaction";
import TransactionDetailDialog from "../../../components/transactions/TransactionDetailDialog";
import DownloadIcon from '@mui/icons-material/Download';
import { toPersianNumber } from "../../../lib/PersianNumberConverter";
import persian from 'react-date-object/calendars/persian';
import persian_fa from 'react-date-object/locales/persian_fa';
import DatePicker, { DateObject } from "react-multi-date-picker";


const columns: Column<Transaction>[] = [
    { key: 'buyerId', label: 'خریدار', render: (value) => `${toPersianNumber(value)}` },
    { key: 'dateTime', label: 'تاریخ', render: (value) => `${toPersianNumber(value)}` },
    { key: 'price', label: 'قیمت', render: (value) => `${toPersianNumber(value.toLocaleString())} تومان` },
];

export default function Transactions() {

    const [startDate, setStartDate] = useState<DateObject | null>()
    const [endDate, setEndDate] = useState<DateObject | null>()
    const [refreshKey, setRefreshKey] = useState(0)

    const [queryParams, setQueryParams] = useState<TransactionFilter>({
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

    const fetchData = useCallback(async (parameters?: TransactionFilter): Promise<ApiResponse<Transaction[]>> => {
        const params: TransactionFilter = {
            pageSize: parameters?.pageSize || 10,
            pageIndex: parameters?.pageIndex || 0,
            search: parameters?.search || '',
            startDate: parameters?.startDate || '',
            endDate: parameters?.endDate || '',
        };
        const response = await getTransactions(params);
        return response;
    }, []);

    const handleFilter = () => {
        if (endDate) {
            const endyear = endDate.toDate().getFullYear();
            const endmonth = String(endDate.toDate().getMonth() + 1).padStart(2, '0'); // Months are 0-based
            const endday = String(endDate.toDate().getDate()).padStart(2, '0');

            setQueryParams(prv => ({
                ...prv,
                endDate: `${endyear}/${endmonth}/${endday}`,
            }))
        }
        if (startDate) {
            const startyear = startDate.toDate().getFullYear();
            const startmonth = String(startDate.toDate().getMonth() + 1).padStart(2, '0'); // Months are 0-based
            const startday = String(startDate.toDate().getDate()).padStart(2, '0');

            setQueryParams(prv => ({
                ...prv,
                startDate: `${startyear}/${startmonth}/${startday}`
            }))
        }

        setRefreshKey(prv=> prv + 1)
    }

    return (
        <>
            <Button
                variant="contained"
                color="primary"
                startIcon={<DownloadIcon sx={{ ml: 1 }} />}
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

            <Box display={"flex"} gap={2} sx={{ flexDirection: { xs: 'column', md: 'row' } }} mb={2}>

                <FormControl fullWidth variant="outlined">
                    <DatePicker
                        id="startDate-picker"
                        calendar={persian}
                        locale={persian_fa}
                        value={startDate}
                        onChange={(date) => {
                            date && setStartDate(date);
                        }}
                        inputClass="custom-date-input"
                        placeholder="تاریخ شروع"
                        style={{ marginTop: 8 }}
                    />
                </FormControl>

                <FormControl fullWidth variant="outlined">
                    <DatePicker
                        id="endDate-picker"
                        disabled={!startDate}
                        calendar={persian}
                        locale={persian_fa}
                        value={endDate}
                        {...(startDate ? { minDate: startDate } : {})}
                        onChange={(date) => {
                            date && setEndDate(date);
                        }}
                        inputClass="custom-date-input"
                        placeholder="تاریخ پایان"
                        style={{ marginTop: 8 }}
                    />
                </FormControl>

                <Button variant="contained" sx={{ width: { xs: '100%', md: '200px' }, mt: 1 }} onClick={handleFilter}>
                    اعمال فیلتر
                </Button>

            </Box>

            <DataTable<Transaction>
                columns={columns}
                fetchData={fetchData} // Pass the fetchData function to the DataTable
                refetch={refetch}
                defaultRowsPerPage={10}
                reloadKey={refreshKey}
                params={queryParams}
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
