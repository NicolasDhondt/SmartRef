import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { ModalModule } from 'ngx-bootstrap/modal';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './screens/common/nav-menu/nav-menu.component';
import { FiltersComponent } from './screens/common/filters/filters.component';
import { HomeComponent } from './screens/home/home.component';
import { FinderReferencesComponent } from './screens/finder/finder-references/finder-references.component';
import { BuilderReferencesComponent } from './screens/builder/builder-references/builder-references.component';
import { BuilderFormComponent } from './screens/builder/builder-form/builder-form.component';
import { BuilderEndComponent } from './screens/builder/builder-end/builder-end.component';
import { DashboardComponent } from './screens/dashboard/dashboard.component';
import { DataQualityComponent } from './screens/data-quality/data-quality.component';
import { TokenComponent } from './token/token.component';
import { ProjectCardComponent } from './screens/finder/project-card/project-card.component';

import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { NgxSkeletonLoaderModule } from 'ngx-skeleton-loader';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    FiltersComponent,
    HomeComponent,
    TokenComponent,
    BuilderReferencesComponent,
    BuilderFormComponent, 
    BuilderEndComponent,
    FinderReferencesComponent,
    ProjectCardComponent,
    DashboardComponent,
    DataQualityComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    ApiAuthorizationModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ModalModule.forRoot(),
    ToastrModule.forRoot({
      positionClass :'toast-bottom-right',
      preventDuplicates: true,
      timeOut:2000
    }),
    NgbModule,
    NgxSkeletonLoaderModule.forRoot({
      animation: 'progress', 
    })
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
