import React, { useEffect, useState } from 'react';
import {
  Drawer,
  List,
  ListItem,
  ListItemIcon,
  ListItemText,
  IconButton,
  Collapse,
  useTheme,
  useMediaQuery,
  Box,
  Tooltip,
  Typography,
} from '@mui/material';
import {
  ChevronRight as ChevronRightIcon,
  ChevronLeft as ChevronLeftIcon,
  ExpandLess,
  ExpandMore,
} from '@mui/icons-material';
import { Icon } from '@iconify/react';
import { useNavigate } from 'react-router-dom'; // Import useNavigate
import menuItems, { MenuItem } from '../config/menuItems'; // Adjust path as needed

interface CollapsibleSidebarProps {
  updateSidebarWidth: (isOpen: boolean) => void;
}

const SidebarMenu: React.FC<CollapsibleSidebarProps> = ({ updateSidebarWidth }) => {
  const [open, setOpen] = useState(true); // Initially open to show full sidebar
  const [expandedMenus, setExpandedMenus] = useState<Record<string, boolean>>({});
  const theme = useTheme();
  const isMobile = useMediaQuery(theme.breakpoints.down('sm'));
  const navigate = useNavigate(); // React Router navigation hook

  useEffect(() => {
    updateSidebarWidth(open);
  }, [open, updateSidebarWidth]);

  useEffect(() => {
    if (isMobile) {
      setOpen(false); // Close the drawer on mobile after navigation
    }
  }, [isMobile])

  const toggleDrawer = () => {
    setOpen((prevOpen) => {
      if (prevOpen) {
        setExpandedMenus({});
      }
      return !prevOpen;
    });
  };

  const handleToggleMenu = (title: string) => {
    setOpen(true); // Expand the sidebar when a menu item is clicked
    setExpandedMenus((prev) => ({
      ...prev,
      [title]: !prev[title],
    }));
  };

  const handleNavigation = (path?: string) => {
    setOpen(true); // Expand the sidebar when a menu item is clicked
    if (path) {
      navigate(path); // Navigate to the provided path
    }
  };

  const renderMenuItems = (items: MenuItem[], level: number = 0) =>
    items.map((item) => (
      <React.Fragment key={item.title}>
        <ListItem
          component="button"
          onClick={() =>
            item.children ? handleToggleMenu(item.title) : handleNavigation(item.path)
          }
          sx={{
            backgroundColor: 'inherit',
            pr: 2 * (level + 1), // Indent dynamically based on level
            textAlign: 'right', // Align text to the right for RTL
            border: 'none', // Remove borders
            '&:hover': {
              backgroundColor: theme.palette.primary.light, // Beautiful hover effect
            },
            color: theme.palette.common.white,
            transition: 'background-color 0.3s ease, color 0.3s ease', // Smooth hover transition
          }}
        >
          <Tooltip title={item.title} placement="right">
            <ListItemIcon sx={{ minWidth: 0, ml: 'auto', color: theme.palette.common.white }}> {/* Align icon to the right */}
              <Icon icon={item.icon} width={20} />
            </ListItemIcon>
          </Tooltip>
          {open && <ListItemText primary={item.title} sx={{ textAlign: 'right', pr: '10px' }} />} {/* Show text only when open */}
          {item.children && open ? expandedMenus[item.title] ? <ExpandLess /> : <ExpandMore /> : null} {/* Show arrows only when open */}
        </ListItem>
        {item.children && (
          <Collapse in={expandedMenus[item.title]} timeout="auto" unmountOnExit>
            <List component="div" disablePadding>
              {renderMenuItems(item.children, level + 1)} {/* Recursively render children */}
            </List>
          </Collapse>
        )}
      </React.Fragment>
    ));

  const items = menuItems;

  return (
    <>
      <Drawer
        anchor="right"
        variant={'persistent'}
        open={true}
        onClose={toggleDrawer}
        ModalProps={{
          keepMounted: true,
        }}
        sx={{
          flexShrink: 0,
          [`& .MuiDrawer-paper`]: {
            width: open ? 240 : 60, // Width 240 when open, 60 when collapsed
            boxSizing: 'border-box',
            right: 0,
            zIndex: 9999,
            left: 'auto',
            direction: 'rtl', // Set the drawer's direction to RTL
            backgroundColor: theme.palette.primary.main,
            transition: 'width 0.3s ease', // Animation for width transition
          },
        }}
      >
        <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', p: 1 }}>
          <Typography sx={{pr:1}} variant="h6" color={'white'} display={!open ? 'none' : 'inline'}>داشبورد</Typography>
          <IconButton onClick={toggleDrawer} sx={{ color: theme.palette.common.white }}>
            {open ? <ChevronRightIcon /> : <ChevronLeftIcon />}
          </IconButton>
        </Box>
        <List sx={{ backgroundColor: 'inherit' }}>{renderMenuItems(items)}</List>
      </Drawer>
    </>
  );
};

export default SidebarMenu;
