import { Box, Button, FormControl } from "@mui/material";
import DataTable, { Column } from "../../../components/common/DataTable";
import { useCallback, useEffect, useState } from "react";
import { ApiResponse } from "../../../types/api";
import { useQuery } from "@tanstack/react-query";
import { ProfitReportData, TransactionFilter } from "../../../types/invoice";
import { downloadProfitReportExcel, getProfitReport } from "../../../api/transaction";
import DownloadIcon from '@mui/icons-material/Download';
import { toPersianNumber } from "../../../lib/PersianNumberConverter";
import DatePicker, { DateObject } from "react-multi-date-picker";
import persian from 'react-date-object/calendars/persian';
import persian_fa from 'react-date-object/locales/persian_fa';

const columns: Column<ProfitReportData>[] = [
    { key: 'productName', label: 'نام محصول' },
    { key: 'availableQuantity', label: 'موجودی فعلی', render: (value) => `${toPersianNumber(value)}` },
    { key: 'soldQuantity', label: 'مجموع فروش', render: (value) => `${toPersianNumber(value)}` },
    { key: 'profit', label: 'سود حاصل از فروش', render: (value) => `${toPersianNumber(value.toLocaleString())} تومان` },
];

export default function ProfitReport() {

    const [startDate, setStartDate] = useState<DateObject | null>()
    const [endDate, setEndDate] = useState<DateObject | null>()
    const [refreshKey, setRefreshKey] = useState(0)


    const [queryParams, setQueryParams] = useState<TransactionFilter>({
        pageSize: 10,
        pageIndex: 0,
        search: ''
    });

    const { refetch } = useQuery<ApiResponse<ProfitReportData[]>, Error>({
        queryKey: ['getProfitReport', queryParams],
        queryFn: () => getProfitReport(queryParams),
        enabled: false
    });

    useEffect(() => {
        refetch();
    }, [queryParams, refetch]);

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

        setRefreshKey(prv => prv + 1)
    }

    const fetchData = useCallback(async (parameters?: TransactionFilter): Promise<ApiResponse<ProfitReportData[]>> => {
        const params: TransactionFilter = {
            pageSize: parameters?.pageSize || 10,
            pageIndex: parameters?.pageIndex || 0,
            search: parameters?.search || '',
            startDate: parameters?.startDate || '',
            endDate: parameters?.endDate || '',
        };
        const response = await getProfitReport(params);
        return response;
    }, []);

    return (
        <>
            <Button
                variant="contained"
                color="primary"
                startIcon={<DownloadIcon sx={{ ml: 1 }} />}
                onClick={() => downloadProfitReportExcel()}
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
                دانلود همه
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

            <DataTable<ProfitReportData>
                columns={columns}
                fetchData={fetchData} // Pass the fetchData function to the DataTable
                refetch={refetch}
                reloadKey={refreshKey}
                params={queryParams}
                defaultRowsPerPage={10}
            />
        </>
    )
}
