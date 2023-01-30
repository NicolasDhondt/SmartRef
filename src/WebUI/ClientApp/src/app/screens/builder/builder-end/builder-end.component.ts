import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ReferenceService } from 'src/app/_services/reference.service';

@Component({
  selector: 'app-builder-end',
  templateUrl: './builder-end.component.html',
  styleUrls: ['./builder-end.component.scss']
})
export class BuilderEndComponent implements OnInit {

  @Input() projectId: number;

  constructor(private referenceService:ReferenceService, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  downloadReference() {
    this.referenceService.downloadRef(this.projectId).subscribe((response) => {
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
