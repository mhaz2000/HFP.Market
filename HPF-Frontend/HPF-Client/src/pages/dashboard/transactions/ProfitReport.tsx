import { Button } from "@mui/material";
import DataTable, { Column } from "../../../components/common/DataTable";
import { useCallback, useEffect, useState } from "react";
import { ApiResponse, DefaultParams } from "../../../types/api";
import { useQuery } from "@tanstack/react-query";
import { ProfitReportData } from "../../../types/invoice";
import { downloadProfitReportExcel, getProfitReport } from "../../../api/transaction";
import DownloadIcon from '@mui/icons-material/Download';
import { toPersianNumber } from "../../../lib/PersianNumberConverter";


const columns: Column<ProfitReportData>[] = [
    { key: 'productName', label: 'نام محصول' },
    { key: 'availableQuantity', label: 'موجودی فعلی', render: (value) => `${toPersianNumber(value)}` },
    { key: 'soldQuantity', label: 'مجموع فروش', render: (value) => `${toPersianNumber(value)}` },
    { key: 'profit', label: 'سود حاصل از فروش', render: (value) => `${toPersianNumber(value.toLocaleString())} تومان` },
];

export default function ProfitReport() {


    const [queryParams] = useState<DefaultParams>({
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

    const fetchData = useCallback(async (parameters?: DefaultParams): Promise<ApiResponse<ProfitReportData[]>> => {
        const params: DefaultParams = {
            pageSize: parameters?.pageSize || 10,
            pageIndex: parameters?.pageIndex || 0,
            search: parameters?.search || ''
        };
        const response = await getProfitReport(params);
        return response;
    }, []);

    return (
        <>
            <Button
                variant="contained"
                color="primary"
                startIcon={<DownloadIcon sx={{ml: 1}}/>}
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
            <DataTable<ProfitReportData>
                columns={columns}
                fetchData={fetchData} // Pass the fetchData function to the DataTable
                refetch={refetch}
                defaultRowsPerPage={10}
            />
        </>
    )
}
