import { Box, Card, CardContent, Container, Typography } from "@mui/material";
import LocalGroceryStoreIcon from '@mui/icons-material/LocalGroceryStore';
import SyncIcon from '@mui/icons-material/Sync';
import { useQuery } from "@tanstack/react-query";
import { getDashboardData } from "../../api/dashboard";

export default function Dashboard() {

  const { data, isLoading } = useQuery({
    queryKey: ['getDashboardData'], // Initialize with an empty object
    queryFn: ({ queryKey }) => {
      const [] = queryKey; // Extract currentParams from queryKey
      return getDashboardData(); // Pass params to API function
    },
  });

  return (
    <Container>
      <Box sx={{ display: 'flex', flexDirection: 'column', alignItems: 'center', backgroundColor: 'inherit', p: 5 }}>
        <Typography textAlign={'center'} variant="h2">داشبورد</Typography>
        <Box width={'100%'} mt={4} display={'flex'} alignItems={'center'} justifyContent={'space-around'} sx={{ flexDirection: { xs: 'column', md: 'row' } }}>
          <Card sx={{ mb: { xs: 4, md: 0 }, display: 'flex', flexDirection: 'column', alignItems: 'center', justifyContent: 'center', backgroundColor: 'inherit', px: '3rem', maxWidth: '500px' }}>
            <LocalGroceryStoreIcon sx={{ fontSize: '8rem' }} />
            <CardContent>
              <Typography variant="h3" textAlign={'center'}>{isLoading ? 0 : data?.totalProducts}</Typography>
              <Typography variant="h5">محصول در فروشگاه</Typography>
            </CardContent>
          </Card>

          <Card sx={{ display: 'flex', flexDirection: 'column', alignItems: 'center', justifyContent: 'center', backgroundColor: 'inherit', px: '3rem', maxWidth: '500px' }}>
            <SyncIcon sx={{ fontSize: '8rem' }} />
            <CardContent>
              <Typography variant="h3" textAlign={'center'}>{isLoading ? 0 : data?.totalTransactions}</Typography>
              <Typography variant="h5">تراکنش انجام شده</Typography>
            </CardContent>
          </Card>
        </Box>
      </Box>
    </Container>
  )
}
