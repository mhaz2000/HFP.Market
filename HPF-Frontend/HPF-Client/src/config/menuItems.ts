export type MenuItem = {
  title: string;
  path?: string;
  icon: string;
  children?: MenuItem[];
};

const menuItems: MenuItem[] = [
  {
    title: 'داشبورد',
    path: '/dashboard',
    icon: 'material-symbols:dashboard',
  },
  {
    title: 'تخفیفات',
    icon: 'mdi:discount',
    children: [
      {
        title: 'افزودن تخفیف جدید',
        path: '/dashboard/new-discount',
        icon: 'mdi:subdirectory-arrow-left',
      },
      {
        title: 'لیست تخفیفات',
        path: '/dashboard/discounts',
        icon: 'mdi:subdirectory-arrow-left',
      }
    ]
  },
  {
    title: 'محصولات',
    icon: 'material-symbols:category',
    children: [
      {
        title: 'افزودن محصول جدید',
        path: '/dashboard/new-product',
        icon: 'mdi:subdirectory-arrow-left',
      },
      {
        title: 'لیست محصولات',
        path: '/dashboard/products',
        icon: 'mdi:subdirectory-arrow-left',
      }
    ]
  },
  {
    title: 'فاکتور خرید',
    icon: 'material-symbols:receipt-long',
    children: [
      {
        title: 'افزودن فاکتور خرید جدید',
        path: '/dashboard/new-purchase-invoice',
        icon: 'mdi:subdirectory-arrow-left',
      },
      {
        title: 'فاکتور های خرید',
        path: '/dashboard/purchase-invoices',
        icon: 'mdi:subdirectory-arrow-left',
      }
    ]
  },
  {
    title: 'گزارشات',
    icon: 'material-symbols:currency-exchange',
    children: [
      {
        title: 'تراکنش های کاربران',
        path: '/dashboard/transactions',
        icon: 'mdi:subdirectory-arrow-left',
      },
      {
        title: 'گزارش فروش',
        path: '/dashboard/profit-report',
        icon: 'mdi:subdirectory-arrow-left',
      }
    ]
  },
  {
    title: 'سازماندهی قفسه ها',
    path: '/dashboard/organizing-shelves',
    icon: 'material-symbols:shelves',
  }
];

export default menuItems;
