import { Injectable } from '@angular/core';
import { CustomersClient, SectorsClient } from '../web-api-client';

@Injectable({
  providedIn: 'root'
})
export class FiltersService {
  
  constructor(private sectorsClient: SectorsClient, private customersClient: CustomersClient) { }

  GetSectors() {
    return this.sectorsClient.getSectors();
  }

  GetCustomers() {
    return this.customersClient.getCustomers();
  }

}
