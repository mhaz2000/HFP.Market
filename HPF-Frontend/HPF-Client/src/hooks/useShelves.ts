// src/hooks/useShelves.ts
import { useState } from "react";
import { Shelf } from "../types/shelf";
import { Product } from "../types/product";

export function useShelves() {
  const [shelves, setShelves] = useState<Shelf[]>([]);

  const addShelf = () => {
    const newShelf: Shelf = {
      id: crypto.randomUUID(),
      order: shelves.length + 1,
      products: [],
    };
    setShelves((prev) => [...prev, newShelf]);
  };

  const removeShelf = (id: string) => {
    setShelves((prev) => prev.filter((s) => s.id !== id));
  };

  const addProductToShelf = (shelfId: string, product: Product) => {
    setShelves((prev) =>
      prev.map((s) =>
        s.id === shelfId
          ? { ...s, products: [...s.products, product] }
          : s
      )
    );
  };

  const removeProductFromShelf = (shelfId: string, productId: string) => {
    setShelves((prev) =>
      prev.map((s) =>
        s.id === shelfId
          ? { ...s, products: s.products.filter((p) => p.id !== productId) }
          : s
      )
    );
  };

  const reorderShelves = (startIndex: number, endIndex: number) => {
    const result = Array.from(shelves);
    const [removed] = result.splice(startIndex, 1);
    result.splice(endIndex, 0, removed);

    // Update order numbers
    result.forEach((s, index) => (s.order = index + 1));
    setShelves(result);
  };

  return {
    shelves,
    addShelf,
    removeShelf,
    addProductToShelf,
    removeProductFromShelf,
    reorderShelves,
  };
}
