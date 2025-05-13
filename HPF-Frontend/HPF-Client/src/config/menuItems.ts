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
      title: 'محصولات',
      path: '/settings',
      icon: 'material-symbols:category',
      children:[
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
  ];
  
  export default menuItems;
  