import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ProjectDTO } from 'src/app/web-api-client';
import { ReferenceService } from 'src/app/_services/reference.service';

@Component({
  selector: 'app-project-card',
  templateUrl: './project-card.component.html',
  styleUrls: ['./project-card.component.scss']
})
export class ProjectCardComponent implements OnInit {

  @Input() project: ProjectDTO;

  constructor(private referenceService: ReferenceService, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  downloadReference(projectId: number) {
    this.referenceService.downloadRef(projectId).subscribe((response) => {
      let filename = response.headers.get('content-disposition')?.split(';')[1].split('=')[1];
      let blob: Blob = response.body as Blob;
      let a = document.createElement('a');
      a.download = filename;
      a.href = window.URL.createObjectURL(blob);
      a.click();
    })
    this.toastr.info("The PowerPoint is in your hands!");
  }

}
