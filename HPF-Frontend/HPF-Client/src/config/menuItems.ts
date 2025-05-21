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
    title: 'تراکنش‌ها',
    path: '/dashboard/transactions',
    icon: 'material-symbols:currency-exchange',
  }
];

export default menuItems;
