import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { CountryDTO, CustomerDTO, SectorDTO, TargetWorkDTO, TechnologyDTO, TribeOfferDTO, VerticalDTO } from 'src/app/web-api-client';
import { BuilderService } from 'src/app/_services/builder.service';
import { FiltersService } from 'src/app/_services/filters.service';

@Component({
  selector: 'app-filters',
  templateUrl: './filters.component.html',
  styleUrls: ['./filters.component.scss']
})
export class FiltersComponent implements OnInit {

  verticals: VerticalDTO[] = [];
  sectors: SectorDTO[] = [];
  customers: CustomerDTO[] = [];
  countries: CountryDTO[] = [];
  targetsWork: TargetWorkDTO[] = [];
  offers: TribeOfferDTO[] = [];
  technologies: TechnologyDTO[] = [];
  isPublicSelected: boolean = true;
  isPrivateSelected: boolean = true;

  @Output() verticalId = new EventEmitter();
  @Output() offerId = new EventEmitter();
  @Output() sectorId = new EventEmitter();
  @Output() customerId = new EventEmitter();
  @Output() countryId = new EventEmitter();
  @Output() technologyId = new EventEmitter();
  @Output() targetId = new EventEmitter();
  @Output() isPrivate = new EventEmitter();
  @Output() isPublic = new EventEmitter();

  constructor(private builderService: BuilderService, private filtersService: FiltersService) { }

  ngOnInit(): void {
    this.loadVerticals();
    this.loadSectors();
    this.loadCustomers();
    this.loadCountries();
    this.loadTargetsWork();
    this.loadOffers();
    this.loadTechnologies();
  }

  private loadVerticals(){
    this.builderService.GetVerticals().subscribe(response => {
      this.verticals = response;
    })
  }

  private loadSectors(){
    this.filtersService.GetSectors().subscribe(response => {
      this.sectors = response;
    })
  }

  private loadCustomers(){
    this.filtersService.GetCustomers().subscribe(response => {
      this.customers = response;
    })
  }

  private loadCountries(){
    this.builderService.GetCountries().subscribe(response => {
      this.countries = response;
    })
  }

  private loadTargetsWork(){
    this.builderService.GetTargetsWork().subscribe(response => {
      this.targetsWork = response;
    })
  }

  private loadOffers(){
    this.builderService.GetTribeOffers().subscribe(response => {
      this.offers = response;
    })
  }

  private loadTechnologies(){
    this.builderService.GetTechnologies().subscribe(response => {
      this.technologies = response;
    })
  }

  verticalChange(value: string){
    this.verticalId.emit(value);
  }

  sectorChange(value: string){
    this.sectorId.emit(value);
  }

  customerChange(value: string){
    this.customerId.emit(value);
  }
  
  targetChange(value: string){
    this.targetId.emit(value);
  }

  offerChange(value: string){
    this.offerId.emit(value);
  }

  technologyChange(value: string){
    this.technologyId.emit(value);
  }

  countryChange(value: string){
    this.countryId.emit(value);
  }

  privateChange(){
    this.isPrivateSelected = !this.isPrivateSelected;
    this.isPrivate.emit(this.isPrivateSelected);
  }

  publicChange(){
    this.isPublicSelected = !this.isPublicSelected;
    this.isPublic.emit(this.isPublicSelected);  
  }

}
