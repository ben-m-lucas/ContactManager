import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.component.html'
})
export class ContactsComponent {
  public contacts: Contact[];
  public form: FormGroup = new FormGroup({
    surname: new FormControl(),
    givenName: new FormControl()
  });

  private http: HttpClient;
  private baseUrl: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.loadContacts();
  }

  loadContacts() {
    this.http.get<Contact[]>(this.baseUrl + 'api/Contacts').subscribe(result => {
      this.contacts = result;
    }, error => console.error(error));
  }

  addContact() {
    const contactToAdd: Contact = {
      surname: this.form.get('surname').value,
      givenName: this.form.get('givenName').value
    };

    this.http.post<Contact>(this.baseUrl + 'api/Contacts', contactToAdd).subscribe(result => {

      this.form.setValue({ surname: '', givenName: '' });
      this.loadContacts();
    });
  }
}

interface Contact {
  surname: string;
  givenName: string;
}
