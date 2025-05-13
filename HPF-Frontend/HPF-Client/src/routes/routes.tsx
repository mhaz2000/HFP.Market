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
          // Add more dashboard routes here
        ],
      },
    ],
  }
];

export default routes;
