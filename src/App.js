import React from 'react';
import './App.css';
import Head from './components/header/Head';
import Notebook from './components/notebook/Notebook'

class App extends React.Component {
  render() {
    return (
      <div className="body">
        <Head/>

        <Notebook/>
        
      </div>
    );
  }
}

export default App;
