import { RouteObject } from 'react-router-dom';
import Home from '../pages/Home';
import About from '../pages/About';
import ProtectedRoute from './ProtectedRoute';
import MainLayout from '../layouts/MainLayout';
import Login from '../pages/Login';
import Dashboard from '../pages/dashboard/Dashboard';
import DashboardLayout from '../layouts/DashboardLayout';
import NewProductPage from '../pages/dashboard/products/NewProduct';
import ProductsTable from '../pages/dashboard/products/Products';
import EditProductPage from '../pages/dashboard/products/EditProductPage';
import Tutorial from '../pages/Tutorial';
import NotFound from '../pages/NotFound';
import Transactions from '../pages/dashboard/transactions/Transactions';
import DiscountsTable from '../pages/dashboard/discounts/Discounts';
import NewDiscountPage from '../pages/dashboard/discounts/NewDiscount';
import ProfitReport from '../pages/dashboard/transactions/ProfitReport';
import EditPurchaseInvoicePage from '../pages/dashboard/purchaseInvoices/EditPurchaseInvoice';
import NewPurchaseInvoicePage from '../pages/dashboard/purchaseInvoices/NewPurchaseInvoice';
import PurchaseInvoices from '../pages/dashboard/purchaseInvoices/PurchaseInvoices';
import OrganizingShelvesPage from '../pages/dashboard/OrganizingShelvesPage';
import Consumers from '../pages/dashboard/Consumers';

const routes: RouteObject[] = [
  {
    path: '/',
    element: <MainLayout />,
    children: [
      { index: true, element: <Home /> },
      { path: 'about', element: <About /> },
      { path: 'login', element: <Login /> },
      { path: 'tutorial', element: <Tutorial /> },
    ],
  },
  {
    path: '/dashboard',
    element: <ProtectedRoute />, // this will guard the route
    children: [
      {
        path: '',
        element: <DashboardLayout />, // this layout wraps all dashboard routes
        children: [
          { index: true, element: <Dashboard /> },
          {
            path: '/dashboard/new-product',
            element: <NewProductPage />
          },
          {
            path: '/dashboard/products',
            element: <ProductsTable />
          },
          {
            path: 'product/:id',
            element: <EditProductPage />
          },
          {
            path: '/dashboard/transactions',
            element: <Transactions />
          },
          {
            path: '/dashboard/profit-report',
            element: <ProfitReport />
          },
          {
            path: '/dashboard/new-discount',
            element: <NewDiscountPage />
          },
          {
            path: '/dashboard/discounts',
            element: <DiscountsTable />
          },
          {
            path: '/dashboard/purchase-invoices',
            element: <PurchaseInvoices />
          },
          {
            path: '/dashboard/new-purchase-invoice',
            element: <NewPurchaseInvoicePage />
          },
          {
            path: '/dashboard/edit-purchase-invoice/:id',
            element: <EditPurchaseInvoicePage />
          },
          {
            path: '/dashboard/organizing-shelves',
            element: <OrganizingShelvesPage />
          },
          {
            path: '/dashboard/consumers',
            element: <Consumers />
          },
          // {
          //   path: 'discount/:id',
          //   element: <EditDiscountPage />
          // },
          // Add more dashboard routes here
        ],
      },
    ],
  },
  {
    path: '*',
    element: <NotFound />,
  },
];

export default routes;
