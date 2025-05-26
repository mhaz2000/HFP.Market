import React, { useState, useEffect } from 'react';
import {
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
    Paper,
    TablePagination,
    IconButton,
    Box,
    TextField,
} from '@mui/material';
import { FirstPage, LastPage, ChevronLeft, ChevronRight } from '@mui/icons-material';
import { ApiResponse, DefaultParams } from '../../types/api';

export interface Column<T> {
    key: keyof T;
    label: string;
    align?: 'left' | 'right' | 'center';
    render?: (value: any, row: T) => React.ReactNode;
}

interface DataTableProps<T> {
    columns: Column<T>[];
    fetchData: (params?: DefaultParams) => Promise<ApiResponse<T[]>>;
    refetch: () => void;
    rowsPerPageOptions?: number[];
    defaultRowsPerPage?: number;
    reloadKey?: number;
    params?: any
    renderActions?: (row: T) => React.ReactNode; // New prop
}

export default function DataTable<T>({
    columns,
    fetchData,
    rowsPerPageOptions = [5, 10, 25],
    defaultRowsPerPage = 10,
    reloadKey,
    params,
    renderActions
}: DataTableProps<T>) {
    const [page, setPage] = useState(0);
    const [rowsPerPage, setRowsPerPage] = useState(defaultRowsPerPage);
    const [rows, setRows] = useState<T[]>([]);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);
    const [totalCount, setTotalCount] = useState(0);
    const [search, setSearch] = useState('');
    const [debouncedSearch, setDebouncedSearch] = useState('');

    // Debounce search input
    useEffect(() => {
        const timeout = setTimeout(() => {
            setDebouncedSearch(search);
            setPage(0); // Reset to first page on search
        }, 500);
        return () => clearTimeout(timeout);
    }, [search]);

    // Fetch data whenever dependencies change
    useEffect(() => {
        const fetchTableData = async () => {
            setIsLoading(true);
            try {
                const defaultParams: DefaultParams = {
                    pageSize: rowsPerPage,
                    pageIndex: page,
                    search: debouncedSearch.trim(),
                };
                const data = await fetchData(params ?? defaultParams);
                setRows(data.data);
                setTotalCount(data.totalCount || 0);
                setError(null);
            } catch (err) {
                setError('Error fetching data');
            } finally {
                setIsLoading(false);
            }
        };
        fetchTableData();
    }, [page, rowsPerPage, debouncedSearch, fetchData, reloadKey]);

    const handleChangePage = (_event: unknown, newPage: number) => setPage(newPage);

    const handleChangeRowsPerPage = (event: React.ChangeEvent<HTMLInputElement>) => {
        setRowsPerPage(parseInt(event.target.value, 10));
        setPage(0);
    };

    return (
        <Paper>
            <Box sx={{ p: 2 }}>
                <TextField
                    fullWidth
                    variant="outlined"
                    size="small"
                    placeholder="جستجو..."
                    value={search}
                    onChange={(e) => setSearch(e.target.value)}
                    sx={{ mb: 2 }}
                />
                <TableContainer>
                    <Table>
                        <TableHead>
                            <TableRow>
                                <TableCell align="right" sx={{fontWeight: '1000'}}>#</TableCell>

                                {columns.map((col) => (
                                    <TableCell key={col.key as string} align={col.align ?? 'right'} sx={{fontWeight: '1000'}}>
                                        {col.label}
                                    </TableCell>
                                ))}

                                {renderActions && (
                                    <TableCell align="center" sx={{fontWeight: '1000'}}>عملیات</TableCell>
                                )}
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {isLoading ? (
                                <TableRow>
                                    <TableCell colSpan={columns.length} align="center">
                                        در حال بارگذاری...
                                    </TableCell>
                                </TableRow>
                            ) : error ? (
                                <TableRow>
                                    <TableCell colSpan={columns.length} align="center">
                                        {error}
                                    </TableCell>
                                </TableRow>
                            ) : rows.length === 0 ? (
                                <TableRow>
                                    <TableCell colSpan={columns.length} align="center">
                                        هیچ داده‌ای برای نمایش وجود ندارد
                                    </TableCell>
                                </TableRow>
                            ) : (
                                rows.map((row, rowIndex) => {
                                    const globalIndex = page * rowsPerPage + rowIndex + 1;
                                    return (
                                        <TableRow key={(row as any).id ?? rowIndex}>
                                            <TableCell align="right">{globalIndex}</TableCell>
                                            {columns.map((col) => (
                                                <TableCell key={col.key as string} align={col.align ?? 'right'}>
                                                    {col.render ? col.render(row[col.key], row) : String(row[col.key])}
                                                </TableCell>
                                            ))}
                                            {renderActions && (
                                                <TableCell align="center">{renderActions(row)}</TableCell>
                                            )}
                                        </TableRow>
                                    );
                                })
                            )}
                        </TableBody>
                    </Table>
                </TableContainer>
            </Box>

            <Box sx={{ display: 'flex', justifyContent: 'center', alignItems: 'center', p: 2 }}>
                <TablePagination
                    component="div"
                    count={totalCount}
                    page={page}
                    onPageChange={handleChangePage}
                    rowsPerPage={rowsPerPage}
                    onRowsPerPageChange={handleChangeRowsPerPage}
                    rowsPerPageOptions={rowsPerPageOptions}
                    labelRowsPerPage="تعداد سطر در هر صفحه:"
                    labelDisplayedRows={({ from, to, count }) =>
                        `${from}–${to} از ${count !== -1 ? count : `بیش از ${to}`}`
                    }
                    dir="rtl"
                    sx={{
                        '.MuiTablePagination-actions': { justifyContent: 'center' },
                        '.MuiTablePagination-selectLabel': { marginRight: 0 },
                        '.MuiTablePagination-select': { backgroundColor: '#f5f5f5' },
                    }}
                    ActionsComponent={(props) => {
                        const { onPageChange, count, page, rowsPerPage } = props;
                        const handleFirst = (e: React.MouseEvent<HTMLButtonElement>) => onPageChange(e, 0);
                        const handleBack = (e: React.MouseEvent<HTMLButtonElement>) => onPageChange(e, page - 1);
                        const handleNext = (e: React.MouseEvent<HTMLButtonElement>) => onPageChange(e, page + 1);
                        const handleLast = (e: React.MouseEvent<HTMLButtonElement>) =>
                            onPageChange(e, Math.max(0, Math.ceil(count / rowsPerPage) - 1));

                        return (
                            <Box sx={{ display: 'flex', justifyContent: 'center', alignItems: 'center', direction: 'rtl' }}>
                                <IconButton onClick={handleFirst} disabled={page === 0} sx={{ color: 'primary.main' }}>
                                    <LastPage />
                                </IconButton>
                                <IconButton onClick={handleBack} disabled={page === 0} sx={{ color: 'primary.main' }}>
                                    <ChevronRight />
                                </IconButton>
                                <IconButton
                                    onClick={handleNext}
                                    disabled={page >= Math.ceil(count / rowsPerPage) - 1}
                                    sx={{ color: 'primary.main' }}
                                >
                                    <ChevronLeft />
                                </IconButton>
                                <IconButton
                                    onClick={handleLast}
                                    disabled={page >= Math.ceil(count / rowsPerPage) - 1}
                                    sx={{ color: 'primary.main' }}
                                >
                                    <FirstPage />
                                </IconButton>
                            </Box>
                        );
                    }}
                />
            </Box>
        </Paper>
    );
}
