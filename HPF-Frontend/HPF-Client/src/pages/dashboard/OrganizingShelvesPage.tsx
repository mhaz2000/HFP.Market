

// src/pages/StoreManagerPage.tsx
import { useEffect, useState } from "react";
import { Grid, Paper, Typography, CircularProgress, Box, Alert } from "@mui/material";
import { useShelves } from "../../hooks/useShelves";
import { getProducts } from "../../api/product";
import { Product } from "../../types/product";
import ShelfList from "../../components/dashboards/shelves/ShelfList";
import ShelfProductList from "../../components/dashboards/shelves/ShelfProductList";

export default function OrganizingShelvesPage() {
    const [products, setProducts] = useState<Product[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    const {
        shelves,
        addShelf,
        removeShelf,
        addProductToShelf,
        removeProductFromShelf,
        reorderShelves,
    } = useShelves();

    useEffect(() => {
        const fetchProducts = async () => {
            try {
                setLoading(true);
                setError(null);

                const res = await getProducts({ pageIndex: 0, pageSize: 9999 });
                setProducts(res.data);
            } catch (err) {
                console.error(err);
                setError("خطا در دریافت لیست محصولات");
            } finally {
                setLoading(false);
            }
        };

        fetchProducts();
    }, []);

    if (loading) {
        return (
            <Box
                display="flex"
                justifyContent="center"
                alignItems="center"
                height="90vh"
            >
                <CircularProgress />
            </Box>
        );
    }

    if (error) {
        return (
            <Box
                display="flex"
                justifyContent="center"
                alignItems="center"
                height="90vh"
            >
                <Alert severity="error">{error}</Alert>
            </Box>
        );
    }

    return (
        <Grid container spacing={2}>
            {/* Left - Shelves */}
            <Grid size={5}>
                <Paper sx={{ p: 2, height: "90vh", overflowY: "auto" }}>
                    <ShelfList
                        shelves={shelves}
                        addShelf={addShelf}
                        removeShelf={removeShelf}
                        reorderShelves={reorderShelves}
                        removeProductFromShelf={removeProductFromShelf}
                    />
                </Paper>
            </Grid>

            {/* Right - Products */}
            <Grid size={7}>
                <Paper sx={{ p: 2, height: "90vh", overflowY: "auto" }}>
                    <Typography variant="h6" gutterBottom>
                        تمام محصولات
                    </Typography>
                    <ShelfProductList
                        products={products}
                        addProductToShelf={addProductToShelf}
                        shelves={shelves}
                    />
                </Paper>
            </Grid>
        </Grid>
    );
}
