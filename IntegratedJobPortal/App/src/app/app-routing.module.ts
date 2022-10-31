import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { PortalListComponent } from './portal-list/portal-list.component';

const routes: Routes = [
  { path: '', component: PortalListComponent, pathMatch: 'full' },
  { path: 'portal', component: PortalListComponent },
  { path: '**', redirectTo:'portal' },
]; 

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes)
  ], 
  exports: [RouterModule]
})
export class AppRoutingModule { }
