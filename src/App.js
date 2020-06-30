import React from 'react';
import './App.css';
import Head from './components/header/Head';
import Notebook from './components/notebook/Notebook'
import ShowNotebook from '../src/components/notebook/ShowNotebook'

class App extends React.Component {
  render() {
    return (
      <div className="body">
        <Head/>

        <button onClick={<ShowNotebook/>}>get data</button>
  
        
        
      </div>
    );
  }
}

export default App;
