import { Component, OnInit } from '@angular/core';
import { PaginatedListOfProjectDTO } from 'src/app/web-api-client';
import { FinderService } from 'src/app/_services/finder.service';

@Component({
  selector: 'app-finder-references',
  templateUrl: './finder-references.component.html',
  styleUrls: ['./finder-references.component.scss']
})
export class FinderReferencesComponent implements OnInit {

  paginatedListOfProjectDTO: PaginatedListOfProjectDTO;
  isLoaded: boolean = false;

  page: number = 1;
  totalCount: number;
  PAGE_SIZE: number = 16;

  // filters
  searchText: string;
  verticalId: string = null;
  sectorId: string = null;
  customerId: string = null;
  technologyId: string = null;
  countryId: string = null;
  offerId: string = null;
  targetId: string = null;
  isPublicSelected: boolean = true;
  isPrivateSelected: boolean = true;

  constructor(private finderService: FinderService) { }

  ngOnInit(): void {
    this.loadProjectsPaginated(this.page);
  }

  loadProjectsPaginated(pageNumber: number){
    this.isLoaded = false;
    this.finderService.GetPaginatedProjects(pageNumber, this.PAGE_SIZE, this.searchText, this.verticalId, this.sectorId, 
      this.customerId, this.technologyId, this.offerId, this.targetId, this.countryId, this.isPrivateSelected, this.isPublicSelected)
      .subscribe(response => {
        this.paginatedListOfProjectDTO = response;
        this.totalCount = this.paginatedListOfProjectDTO.totalCount;
        this.isLoaded = true;
      })
  }

  verticalFilterChange(event: string){
    if(event === ""){
      this.verticalId = null;
    }else{
      this.verticalId = event;
    }
    this.loadProjectsPaginated(this.page);
  }

  sectorFilterChange(event: string){
    if(event === ""){
      this.sectorId = null;
    }else{
      this.sectorId = event;
    }
    this.loadProjectsPaginated(this.page);
  }

  customerFilterChange(event: string){
    if(event === ""){
      this.customerId = null;
    }else{
      this.customerId = event;
    }
    this.loadProjectsPaginated(this.page);
  }

  offerFilterChange(event: string){
    if(event === ""){
      this.offerId = null;
    }else{
      this.offerId = event;
    }
    this.loadProjectsPaginated(this.page);
  }

  technologyFilterChange(event: string){
    if(event === ""){
      this.technologyId = null;
    }else{
      this.technologyId = event;
    }
    this.loadProjectsPaginated(this.page);
  }

  targetFilterChange(event: string){
    if(event === ""){
      this.targetId = null;
    }else{
      this.targetId = event;
    }
    this.loadProjectsPaginated(this.page);
  }

  countryFilterChange(event: string){
    if(event === ""){
      this.countryId = null;
    }else{
      this.countryId = event;
    }
    this.loadProjectsPaginated(this.page);
  }

  privateFilterChange(event: boolean){
    this.isPrivateSelected = event;
    this.loadProjectsPaginated(this.page);
  }

  publicFilterChange(event: boolean){
    this.isPublicSelected = event;
    this.loadProjectsPaginated(this.page);
  }

}
