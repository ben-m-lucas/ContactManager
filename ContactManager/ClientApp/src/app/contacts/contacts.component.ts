import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.component.html'
})
export class ContactsComponent {
  public contacts: Contact[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Contact[]>(baseUrl + 'api/Contacts').subscribe(result => {
      this.contacts = result;
    }, error => console.error(error));
  }
}

interface Contact {
  surname: string;
  givenName: string;
}
