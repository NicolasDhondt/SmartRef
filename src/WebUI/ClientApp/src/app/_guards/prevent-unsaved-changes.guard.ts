import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { Observable } from 'rxjs';
import { BuilderReferencesComponent } from '../screens/builder/builder-references/builder-references.component';
import { ConfirmService } from '../_services/confirm.service';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangesGuard implements CanDeactivate<unknown> {
  
  constructor(private confirmService: ConfirmService) {}

  canDeactivate(component: BuilderReferencesComponent): Observable<boolean> |  boolean {
    if(component.projectId === 0
      && component.projectFormComponent.projectForm.dirty 
      && component.projectFormComponent.projectForm.valid){
      return this.confirmService.confirm("Confirmation", "Are you sure to leave? Your changes will be lost.", "Ok", "Cancel");
    }
    return true;
  }
  
}
