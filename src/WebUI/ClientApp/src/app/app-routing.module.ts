import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './screens/home/home.component';
import { DashboardComponent } from './screens/dashboard/dashboard.component';
import { DataQualityComponent } from './screens/data-quality/data-quality.component';
import { FinderReferencesComponent } from './screens/finder/finder-references/finder-references.component';
import { BuilderReferencesComponent } from './screens/builder/builder-references/builder-references.component';
import { PreventUnsavedChangesGuard } from './_guards/prevent-unsaved-changes.guard';

export const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'dataquality', component: DataQualityComponent },
  { path: 'finder', component: FinderReferencesComponent },
  { path: 'builder', component: BuilderReferencesComponent, canDeactivate: [PreventUnsavedChangesGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
