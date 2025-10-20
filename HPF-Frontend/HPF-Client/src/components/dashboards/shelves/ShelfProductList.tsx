import {
  Box,
  Card,
  CardContent,
  Typography,
  Select,
  MenuItem,
  List,
  ListItem,
  ListItemText,
  Divider,
  Tooltip,
} from "@mui/material";
import { Product } from "../../../types/product";
import { Shelf } from "../../../types/shelf";

interface Props {
  products: Product[];
  addProductToShelf: (shelfId: string, product: Product) => void;
  shelves: Shelf[];
}

export default function ShelfProductList({
  products,
  addProductToShelf,
  shelves,
}: Props) {
  const handleAdd = (product: Product, shelfId: string) => {
    if (!shelfId) return;
    addProductToShelf(shelfId, product);
  };

  const findShelfOfProduct = (productId: string) =>
    shelves.find((s) => s.products.some((p) => p.id === productId));

  return (
    <Box dir="rtl">
      <Typography variant="h6" mb={2}>
        لیست محصولات
      </Typography>

      <Card variant="outlined" sx={{ borderRadius: 3, boxShadow: 1 }}>
        <CardContent sx={{ p: 0 }}>
          <List sx={{ p: 0 }}>
            {products.map((product, index) => {
              const productShelf = findShelfOfProduct(product.id);
              const isAssigned = !!productShelf;

              return (
                <Box key={product.id}>
                  <ListItem
                    sx={{
                      width: "100%",
                      display: "flex",
                      flexDirection: "row-reverse", // ⬅️ ensures RTL layout
                      justifyContent: "space-between",
                      alignItems: "center",
                      px: 2,
                      py: 1,
                      textAlign: "right",
                    }}
                  >
                    <Tooltip title={product.name} placement="top" arrow>
                      <ListItemText
                        primary={
                          <Typography
                            variant="subtitle2"
                            noWrap
                            sx={{
                              fontWeight: 500,
                              textAlign: "right",
                            }}
                          >
                            {product.name} - موجودی: {product.quantity}
                          </Typography>
                        }

                        sx={{
                          flex: 1,
                          mr: 2,
                          textAlign: "right",
                        }}
                      />
                    </Tooltip>

                    {shelves.length > 0 && (
                      <Select
                        size="medium"
                        displayEmpty
                        sx={{
                          minWidth: 200,
                          fontSize: 13,
                          bgcolor: "#fafafa",
                          borderRadius: 2,
                          direction: "rtl", // ⬅️ force dropdown text RTL
                          textAlign: "right",
                        }}
                        value={productShelf ? productShelf.id : ""}
                        onChange={(e) =>
                          handleAdd(product, e.target.value as string)
                        }
                        disabled={isAssigned}
                      >
                        {!isAssigned && (
                          <MenuItem value="">
                            افزودن به قفسه...
                          </MenuItem>
                        )}
                        {shelves.map((s) => (
                          <MenuItem
                            key={s.id}
                            value={s.id}
                            sx={{ direction: "rtl" }}
                          >
                            قفسه {s.order}
                          </MenuItem>
                        ))}
                      </Select>
                    )}
                  </ListItem>

                  {index < products.length - 1 && <Divider />}
                </Box>
              );
            })}

            {products.length === 0 && (
              <Typography
                variant="body2"
                color="text.secondary"
                align="center"
                sx={{ py: 3 }}
              >
                محصولی برای نمایش وجود ندارد
              </Typography>
            )}
          </List>
        </CardContent>
      </Card>
    </Box>
  );
}
