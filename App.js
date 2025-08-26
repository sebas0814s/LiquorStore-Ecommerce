import React from 'react';
import { initializeApp } from 'firebase/app';

import LoginScreen from './src/screens/LoginScreen';
import { firebaseConfig } from './firebaseConfig';

initializeApp(firebaseConfig);

export default function App() {
  return <LoginScreen />;
}
