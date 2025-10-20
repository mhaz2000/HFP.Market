import {
  Box,
  Button,
  IconButton,
  List,
  ListItem,
  Typography,
  Divider,
} from "@mui/material";
import { Delete, ArrowUpward, ArrowDownward } from "@mui/icons-material";
import { Shelf } from "../../../types/shelf";
import { Product } from "../../../types/product";

interface Props {
  shelves: Shelf[];
  addShelf: () => void;
  removeShelf: (id: string) => void;
  reorderShelves: (startIndex: number, endIndex: number) => void;
  removeProductFromShelf: (shelfId: string, productId: string) => void;
}

export default function ShelfList({
  shelves,
  addShelf,
  removeShelf,
  reorderShelves,
  removeProductFromShelf,
}: Props) {
  const moveUp = (index: number) => {
    if (index <= 0) return;
    reorderShelves(index, index - 1);
  };

  const moveDown = (index: number) => {
    if (index >= shelves.length - 1) return;
    reorderShelves(index, index + 1);
  };

  return (
    <Box dir="rtl">
      <Box display="flex" justifyContent="space-between" alignItems="center" mb={1}>
        <Typography variant="h6">قفسه‌ها</Typography>
        <Button variant="contained" size="small" onClick={addShelf}>
          افزودن قفسه
        </Button>
      </Box>

      <List>
        {shelves
          .slice()
          .sort((a, b) => a.order - b.order)
          .map((shelf, index) => (
            <ListItem
              key={shelf.id}
              sx={{
                border: "1px solid #ddd",
                borderRadius: 2,
                mb: 1,
                flexDirection: "column",
                alignItems: "stretch",
                p: 1,
                direction: "rtl",
              }}
            >
              <Box display="flex" justifyContent="space-between" alignItems="center" mb={1}>
                <Typography variant="subtitle2">قفسه {shelf.order}</Typography>

                <Box display="flex" gap={0.5}>
                  <IconButton
                    size="small"
                    onClick={() => moveUp(index)}
                    disabled={index === 0}
                    aria-label="حرکت به بالا"
                  >
                    <ArrowUpward fontSize="small" />
                  </IconButton>

                  <IconButton
                    size="small"
                    onClick={() => moveDown(index)}
                    disabled={index === shelves.length - 1}
                    aria-label="حرکت به پایین"
                  >
                    <ArrowDownward fontSize="small" />
                  </IconButton>

                  <IconButton
                    size="small"
                    color="error"
                    onClick={() => removeShelf(shelf.id)}
                    aria-label="حذف قفسه"
                  >
                    <Delete fontSize="small" />
                  </IconButton>
                </Box>
              </Box>

              <Divider />

              <Box mt={1}>
                {shelf.products.length === 0 ? (
                  <Typography variant="body2" color="text.secondary">
                    بدون محصول
                  </Typography>
                ) : (
                  shelf.products.map((p: Product) => (
                    <Box
                      key={p.id}
                      sx={{
                        display: "flex",
                        justifyContent: "space-between",
                        alignItems: "center",
                        py: 0.5,
                        borderBottom: "1px dashed rgba(0,0,0,0.04)",
                        direction: "rtl",
                      }}
                    >
                      <Typography variant="body2" noWrap>
                        {p.name}
                      </Typography>
                      <IconButton
                        size="small"
                        color="error"
                        onClick={() => removeProductFromShelf(shelf.id, p.id)}
                        aria-label="حذف محصول"
                      >
                        <Delete fontSize="small" />
                      </IconButton>
                    </Box>
                  ))
                )}
              </Box>
            </ListItem>
          ))}
      </List>
    </Box>
  );
}
