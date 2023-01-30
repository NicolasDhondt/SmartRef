import { Component, OnInit, ViewChild } from '@angular/core';
import { BuilderFormComponent } from '../builder-form/builder-form.component';

@Component({
  selector: 'app-builder-references',
  templateUrl: './builder-references.component.html',
  styleUrls: ['./builder-references.component.scss']
})
export class BuilderReferencesComponent implements OnInit {

  @ViewChild("projectFormComponent", { static: false }) projectFormComponent: BuilderFormComponent;
  projectId: number = 0;

  constructor() { }

  ngOnInit(): void { }

  formCompleted(event: number){
    this.projectId = event;
  }

}
