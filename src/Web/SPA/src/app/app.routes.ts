import { Routes } from '@angular/router';
import { PropertyListComponent } from './property/PropertyList/property-list.component';
import { AuthComponent } from './auth/auth.component';
import { WishlistComponent } from './wishlist/wishlist.component';
import { AppLayoutComponent } from './app-layout/app-layout.component';
import { PageNotFoundComponent } from './Shared/components/page-not-found/page-not-found.component';
import { PropertyDetailComponent } from './property/property-detail/property-detail.component';

export const routes: Routes = [
  {
    path: '',
    component: AppLayoutComponent,
  },
  {
    path: 'wishlist',
    component: WishlistComponent,
  },
  {
    path: 'property',
    children: [
      {
        path: '',
        component: PropertyListComponent,
      },
      {
        path: ':id',
        component: PropertyDetailComponent,
      },
    ],
  },
  {
    path: 'login',
    component: AuthComponent,
  },
  { path: '**', component: PageNotFoundComponent },
];
