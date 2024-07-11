
import { getFirestore } from "firebase/firestore";

// Import the functions you need from the SDKs you need
import { initializeApp } from "firebase/app";
import { getAnalytics } from "firebase/analytics";
// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries

// Your web app's Firebase configuration
// For Firebase JS SDK v7.20.0 and later, measurementId is optional
const firebaseConfig = {
  apiKey: "AIzaSyBTLsYiCG5pI3XADaE3Jhx84JjguQbUOHs",
  authDomain: "medminder-9f003.firebaseapp.com",
  databaseURL: "https://medminder-9f003-default-rtdb.asia-southeast1.firebasedatabase.app",
  projectId: "medminder-9f003",
  storageBucket: "medminder-9f003.appspot.com",
  messagingSenderId: "61026397720",
  appId: "1:61026397720:web:00b8321aaef03b32ea1d6d",
  measurementId: "G-MRDVW3L0L4"
};

// Initialize Firebase
const app = initializeApp(firebaseConfig);
const analytics = getAnalytics(app);



export const db = getFirestore(app);
