import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet, 
    CommonModule, 
    HttpClientModule, 
    FormsModule, 
    ReactiveFormsModule, 
    MatInputModule, 
    MatSelectModule, 
    MatButtonModule
  ], 
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  tasks: any[] = [];
  APIURL = "http://localhost:5121/api/v1/Patient/GetAllPatients";
  POSTURL = "http://localhost:5121/api/v1/Patient";
  DELETEURL = "http://localhost:5121/api/v1/Patient"; // Assuming DELETE URL is the same

  displayedColumns: string[] = ['firstName', 'lastName', 'city', 'active', 'actions'];
  newTask = {
    firstName: '',
    lastName: '',
    city: '',
    active: '',
  };

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.getTasks();
  }

  getTasks() {
    this.http.get<any[]>(this.APIURL).subscribe(
      (res) => {
        this.tasks = res; // Assuming API returns an array directly
      },
      (error) => {
        console.error('Error fetching tasks:', error);
      }
    );
  }

  addTask() {
    this.http.post<any>(this.POSTURL, this.newTask).subscribe(
      (res) => {
        this.tasks.push(res); // Assuming the API returns the created object
        this.tasks = [...this.tasks]; // Update the tasks array to trigger change detection
        this.newTask = {
          firstName: '',
          lastName: '',
          city: '',
          active: '',
        };
      },
      (error) => {
        console.error('Error adding task:', error);
      }
    );
  }

  

  deleteTask(index: number) {
    const task = this.tasks[index];
    const deleteURL = `${this.DELETEURL}/${task.patientId}`; // Assuming each task has a unique id

    this.http.delete(deleteURL).subscribe(
      (res) => {
        this.tasks.splice(index, 1); // Remove the task from the array
        this.tasks = [...this.tasks]; // Update the tasks array to trigger change detection
      },
      (error) => {
        console.error('Error deleting task:', error);
      }
    );
  }
}
