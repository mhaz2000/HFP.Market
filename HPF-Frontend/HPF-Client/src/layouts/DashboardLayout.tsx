import { useTheme, useMediaQuery, CssBaseline, Box, Toolbar } from '@mui/material';
import { useState } from 'react';
import SidebarMenu from './SidebarMenu';
import { Outlet } from 'react-router-dom';
import DashboardNavbar from './DashboardNavbar';

export default function DashboardLayout() {
    const [isSidebarOpen, setIsSidebarOpen] = useState(true);
    const theme = useTheme();
    const isMobile = useMediaQuery(theme.breakpoints.down('sm'));

    const sidebarWidth = isSidebarOpen ? 240 : 60;

    return (
        <>
            <CssBaseline />
            <Box sx={{ display: 'flex', direction: 'rtl' }}>
                {/* Sidebar */}
                <SidebarMenu updateSidebarWidth={setIsSidebarOpen} />

                {/* Main Content */}
                <Box
                    component="main"
                    sx={{
                        flexGrow: 1,
                        p: 3,
                        mr: isMobile ? 0 : `${sidebarWidth}px`, // RTL: main content shifts accordingly
                        transition: 'margin 0.3s ease',
                        width: `calc(100% - ${isMobile ? 0 : sidebarWidth}px)`,
                    }}
                >
                    <DashboardNavbar />
                    <Toolbar />
                    <Outlet />
                </Box>
            </Box>
        </>
    );
}
