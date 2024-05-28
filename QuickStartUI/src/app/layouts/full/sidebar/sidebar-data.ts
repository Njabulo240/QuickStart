import { NavItem } from './nav-item/nav-item';

export const navItems: NavItem[] = [
  {
    navCap: 'Home',
  },
  {
    displayName: 'Dashboard',
    iconName: 'layout-dashboard',
    route: '/dashboard',
  },
  {
    navCap: 'Administration',
  },
  {
    displayName: 'Roles',
    iconName: 'poker-chip',
    route: '/ui-components/roles',
  },
  {
    displayName: 'Users',
    iconName: 'rosette',
    route: '/ui-components/users',
  },

  {
    displayName: 'Customers',
    iconName: 'list',
    route: '/ui-components/lists',
  },
  {
    displayName: 'Audit logs',
    iconName: 'layout-navbar-expand',
    route: '/ui-components/audits',
  },
  {
    displayName: 'About',
    iconName: 'tooltip',
    route: '/ui-components/about',
  },

];
