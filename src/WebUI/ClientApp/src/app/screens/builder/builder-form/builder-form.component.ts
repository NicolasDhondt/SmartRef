import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ContactDTO, ContactType, CountryDTO, CustomerDTO, SectorDTO, TagDTO, TargetWorkDTO, TechnologyDTO, TribeOfferDTO, VerticalDTO } from 'src/app/web-api-client';
import { noEmptyValidator } from 'src/app/_helpers/builderValidatorsHelper';
import { BuilderService } from 'src/app/_services/builder.service';
import { ReferenceService } from 'src/app/_services/reference.service';

@Component({
  selector: 'app-builder-form',
  templateUrl: './builder-form.component.html',
  styleUrls: ['./builder-form.component.scss']
})
export class BuilderFormComponent implements OnInit {

  projectForm: FormGroup;
  currentProcess: number;
  timeLeft: number;
  @Output() projectId = new EventEmitter();

  // data to print in the html
  verticals: VerticalDTO[] = [];
  sectors: SectorDTO[] = [];
  customers: CustomerDTO[] = [];
  countries: CountryDTO[] = [];
  countriesSelectedToPrint: CountryDTO[] = [];
  targetsWork: TargetWorkDTO[] = [];
  tags: TagDTO[] = [];
  offers: TribeOfferDTO[] = [];
  technologies: TechnologyDTO[] = [];
  technologiesSelectedToPrint: TechnologyDTO[] = [];
  contactsDelivery: ContactDTO[] = [];
  contactsSales: ContactDTO[] = [];
  contactsDeliverySelectedToPrint: ContactDTO[] = [];
  contactsSalesSelectedToPrint: ContactDTO[] = [];
  
  // tabs of ids of the selected tags options
  countriesIds: number[] = [];
  tagsIds: number[] = [];
  targetsWorkIds: number[] = [];
  offersIds: number[] = [];
  technologiesIds: number[] = [];
  contactsSalesIds: number[] = [];
  contactsDeliveryIds: number[] = [];

  constructor(private builderService: BuilderService, private referenceService: ReferenceService,
    private formBuilder: FormBuilder, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.currentProcess = 0;
    this.timeLeft = 12;
    this.initGroupForm();
    this.initValuesChangeObserver(this.projectForm.controls.issues, 3);
    this.initValuesChangeObserver(this.projectForm.controls.benefits, 3);
    this.initValuesChangeObserver(this.projectForm.controls.solutions, 4);
    this.loadVerticals();
    this.loadCountries();
    this.loadTargetsWork();
    this.loadTags();
    this.loadTribeOffers();
    this.loadTechnologies();
    this.loadSalesContact();
    this.loadDeliveryContact();
  }

  private initGroupForm(){
    this.projectForm = this.formBuilder.group({
      verticalId: [[Validators.required]], // here a short FormControl
      sectorId: [[Validators.required]],
      customerId: [[Validators.required]],
      name: ['', [Validators.required, noEmptyValidator()]],
      endYear: [2150, [Validators.required, Validators.min(1950), Validators.max(2150)]],
      isFinished: [true, [Validators.required]],
      price: [100000, [Validators.required, Validators.min(0)]],
      manDay: [10, [Validators.required, Validators.min(0), Validators.max(2000)]],
      issues: [''],
      solutions: [''],
      benefits: [''],
      narative: [''],
      isPublic:  [true, [Validators.required]]
    })
  }

  private initValuesChangeObserver(control: AbstractControl, maxCount: number){
    control.valueChanges.subscribe(() => {
      const value: string = control.value;
      let count = value.split('\n').length;
        if(count > maxCount){
          control.setValue(value.substring(0,value.length-1));
          this.toastr.error("Only " + maxCount + " paragraph are authorized for this input");
        }
    })
  }

  isFirstPart(): boolean{
    return this.currentProcess == 0 && this.timeLeft == 12;
  }

  isSecondPart(): boolean{
    return this.currentProcess == 10 && this.timeLeft == 10;
  }

  isThirdPart(): boolean{
    return this.currentProcess == 30  && this.timeLeft == 7;
  }

  isLastPart(): boolean{
    return this.currentProcess == 80 && this.timeLeft == 2;
  }

  nextPart(){
    switch(this.currentProcess){
      case 0:
        this.currentProcess = 10; // up the process
        this.timeLeft = 10; // down the time left
        break;
      case 10:
        this.currentProcess = 30;
        this.timeLeft = 7;
        break;
      case 30:
        this.currentProcess = 80;
        this.timeLeft = 2;
        break;
      case 80:
        this.currentProcess = 100;
        this.timeLeft = 0;
        break;
      default: 
        this.currentProcess = 0;
        this.timeLeft = 12;
        break;
    }
  }

  previousPart(){
    switch(this.currentProcess){
      case 10:
        this.currentProcess = 0; // down the process
        this.timeLeft = 12; // up the time left
        break;
      case 30:
        this.currentProcess = 10;
        this.timeLeft = 10;
        break;
      case 80:
        this.currentProcess = 30;
        this.timeLeft = 7;
        break;
      case 100:
        this.currentProcess = 80;
        this.timeLeft = 2;
        break;
      default: 
        this.currentProcess = 0;
        this.timeLeft = 12;
        break;
    }
  }

  canGoNext(): boolean{
    return (this.projectForm.controls['verticalId'].value != null 
      && this.projectForm.controls['sectorId'].value != null 
      && this.projectForm.controls['customerId'].value != null && this.isFirstPart())
    || (this.projectForm.controls['name'].value != null 
      && this.projectForm.controls['name'].value != ""
      && this.projectForm.controls['name'].value.trim().length !== 0
      && this.projectForm.controls['endYear'].value >= 1950 
      && this.projectForm.controls['endYear'].value <= 2150
      && this.projectForm.controls['price'].value > 0  
      && this.projectForm.controls['manDay'].value > 0 && this.isSecondPart())
    || (this.technologiesIds.length > 0 && this.isThirdPart());
  }

  private loadVerticals(){
    this.builderService.GetVerticals().subscribe(response => {
      this.verticals = response;
      this.sectors = [];
      this.projectForm.controls['sectorId'].reset();
      if(this.verticals.length !== 0){
        this.loadSectors(this.verticals[0].id.toString());
        this.projectForm.controls['verticalId'].setValue(this.verticals[0].id);
      }
    })
  }

  loadSectors(verticalIdText: string){
    let verticalId = Number.parseInt(verticalIdText);
    this.builderService.GetSectorsByVertical(verticalId).subscribe(response => {
      this.sectors = response;
      this.customers = [];
      this.projectForm.controls['customerId'].reset();
      if(response.length !== 0){
        this.loadCustomers(response[0].id.toString());
        this.projectForm.controls['sectorId'].setValue(response[0].id);
      }
    })
  }

  loadCustomers(sectorIdText: string){
    let sectorId = Number.parseInt(sectorIdText);
    this.builderService.GetCustomersBySector(sectorId).subscribe(response => {
      this.customers = response;
      if(response.length !== 0){
        this.projectForm.controls['customerId'].setValue(response[0].id);
      }
    })
  }

  private loadCountries(){
    this.builderService.GetCountries().subscribe(response => {
      this.countries = response;
      if(this.countries.length !== 0){
        this.addCountry(this.countries[0].id.toString());
      }
    })
  }

  private loadTargetsWork(){
    this.builderService.GetTargetsWork().subscribe(response => {
      this.targetsWork = response;
    })
  }

  private loadTags(){
    this.builderService.GetTags().subscribe(response => {
      this.tags = response;
    })
  }

  private loadTribeOffers(){
    this.builderService.GetTribeOffers().subscribe(response => {
      this.offers = response;
    })
  }

  private loadTechnologies(){
    this.builderService.GetTechnologies().subscribe(response => {
      this.technologies = response;
      if(this.technologies.length !== 0){
        this.addTechnology(this.technologies[0].id.toString());
      }
    })
  }

  private loadSalesContact(){
    this.builderService.GetContactsByType(ContactType.SALES).subscribe(response => {
      this.contactsSales = response;
      if(this.contactsSales.length !== 0){
        this.addContact(this.contactsSales[0].id.toString(), ContactType.SALES);
      }
    })
  }

  private loadDeliveryContact(){
    this.builderService.GetContactsByType(ContactType.DELIVERY).subscribe(response => {
      this.contactsDelivery = response;
      if(this.contactsDelivery.length !== 0){
        this.addContact(this.contactsDelivery[0].id.toString(), ContactType.DELIVERY);
      }
    })
  }

  addTechnology(technologyIdText: string){
    let technologyId = Number.parseInt(technologyIdText);
    if(!this.technologiesIds.includes(technologyId)){
      this.builderService.GetTechnologyById(technologyId).subscribe(response => {
        this.technologiesSelectedToPrint.push(response);
        this.technologiesIds.push(technologyId);
      })
    }
  }

  removeTechnology(technologyId: number){
    if(this.technologiesIds.includes(technologyId)){
      let index = this.technologiesIds.indexOf(technologyId);
      this.technologiesIds.splice(index, 1);
      this.technologiesSelectedToPrint.splice(index, 1);
    }
  }

  addCountry(countryIdText: string){
    let countryId = Number.parseInt(countryIdText);
    if(!this.countriesIds.includes(countryId)){
      this.builderService.GetCountryById(countryId).subscribe(response => {
        this.countriesSelectedToPrint.push(response);
        this.countriesIds.push(countryId);
      })
    }
  }

  removeCountry(countryId: number){
    if(this.countriesIds.includes(countryId)){
      let index = this.countriesIds.indexOf(countryId);
      this.countriesIds.splice(index, 1);
      this.countriesSelectedToPrint.splice(index, 1);
    }
  }

  addContact(contactIdText: string, contactType: ContactType){
    let contactId = Number.parseInt(contactIdText);
    this.builderService.GetContactById(contactId).subscribe(response => {
      if(contactType == 0 && !this.contactsSalesIds.includes(contactId)){
        this.contactsSalesSelectedToPrint.push(response);
        this.contactsSalesIds.push(contactId);
      }
      if(contactType == 1 && !this.contactsDeliveryIds.includes(contactId)){
        this.contactsDeliverySelectedToPrint.push(response);
        this.contactsDeliveryIds.push(contactId);
      }
    })
  }

  removeContact(contactId: number, contactType: ContactType){
    if(contactType == 0){
      if(this.contactsSalesIds.includes(contactId)){
        let index = this.contactsSalesIds.indexOf(contactId);
        this.contactsSalesIds.splice(index, 1);
        this.contactsSalesSelectedToPrint.splice(index, 1);
      }
    }
    if(contactType == 1){
      if(this.contactsDeliveryIds.includes(contactId)){
        let index = this.contactsDeliveryIds.indexOf(contactId);
        this.contactsDeliveryIds.splice(index, 1);
        this.contactsDeliverySelectedToPrint.splice(index, 1);
      }
    }
  }

  newTargetWork(targetWorkId: number){
    if(!this.targetsWorkIds.includes(targetWorkId)){
      this.targetsWorkIds.push(targetWorkId);
    }else{
      let index = this.targetsWorkIds.indexOf(targetWorkId); 
      if (index !== -1) {
        this.targetsWorkIds.splice(index, 1);
      }
    }
  }

  newTag(tagId: number){
    if(!this.tagsIds.includes(tagId)){
      this.tagsIds.push(tagId);
    }else{
      let index = this.tagsIds.indexOf(tagId); 
      if (index !== -1) {
        this.tagsIds.splice(index, 1);
      }
    }
  }

  newOffer(offerId: number){
    if(!this.offersIds.includes(offerId)){
      this.offersIds.push(offerId);
    }else{
      let index = this.offersIds.indexOf(offerId); 
      if (index !== -1) {
        this.offersIds.splice(index, 1);
      }
    }
  }

  saveProject(){
    this.referenceService.addReference(this.projectForm.value).subscribe(response => { // response is the id of the project created
      let projectId = response as number;
      this.countriesIds.forEach(element => { // element is one id in the tab
        this.referenceService.createLinkProjectCountry(projectId, element).subscribe(response =>{
          this.countriesIds = [] ;
          this.countriesSelectedToPrint = [];
          this.addCountry("1");
        });
      });
      this.tagsIds.forEach(element => {
        this.referenceService.createLinkProjectTag(projectId, element).subscribe(response =>{
          this.tagsIds = [] ;
        });
      });
      this.targetsWorkIds.forEach(element => {
        this.referenceService.createLinkProjectTargetWork(projectId, element).subscribe(response =>{
          this.targetsWorkIds = [] ;
        });
      });
      this.offersIds.forEach(element => {
        this.referenceService.createLinkProjectOffer(projectId, element).subscribe(response =>{
          this.offersIds = [] ;
        });
      });
      this.technologiesIds.forEach(element => {
        this.referenceService.createLinkProjectTechnology(projectId, element).subscribe(response =>{
          this.technologiesIds = [] ;
          this.technologiesSelectedToPrint = [];
          this.addTechnology("1");
        });
      });
      this.contactsSalesIds.forEach(element => {
        this.referenceService.createLinkProjectContact(projectId, element, 0).subscribe(response =>{
          this.contactsSalesIds = [] ;
          this.contactsSalesSelectedToPrint = [];
          this.addContact("1", 0);
        });
      });
      this.contactsDeliveryIds.forEach(element => {
        this.referenceService.createLinkProjectContact(projectId, element, 1).subscribe(response =>{
          this.contactsDeliveryIds = [] ;
          this.contactsDeliverySelectedToPrint = [];
          this.addContact("1", 1);        
        });
      });
      this.toastr.success("Project added");
      this.projectId.emit(projectId);
    })
  }

  /* private findInvalidControls() {
    const controls = this.projectForm.controls;
    for (const name in controls) {
      if (controls[name].invalid) {
        console.log(name);
      }
    }
  } */

}
