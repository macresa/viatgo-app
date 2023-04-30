import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SearchComponent } from './components/search/search.component';
import { HomeComponent } from './components/home/home.component';
import { CheckoutComponent } from './components/checkout/checkout.component';
import { AuthGuard } from '../guards/auth.guard';
import { MyflightComponent } from './components/myflight/myflight.component';

const routes: Routes = [
   {path: '', component: HomeComponent},
   {path: 'search', component: SearchComponent},
   {path: 'checkout', component: CheckoutComponent, canActivate: [AuthGuard]},
   {path: 'myflight', component: MyflightComponent, canActivate: [AuthGuard]},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FlightsRoutingModule { }
