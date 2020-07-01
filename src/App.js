import React from 'react';
import './App.css';
import Head from './components/header/Head';
import Notebook from './components/notebook/Notebook'
import ShowNotebook from '../src/components/notebook/ShowNotebook'
import ShowAllNotebook from './components/notebook/ShowAllNotebooks';

class App extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      id: '',
    }
  }

  setId = (id) => {
    this.setState({ id: id });
  }

  render() {
    return (
      <div className="body">
        <Head />
        <input value="" onChange={(event) => this.setId(event.target.value)} />
        {this.state.id != '' && <ShowNotebook id={this.state.id} />}
        <ShowAllNotebook/>
      </div>
    );
  }
}

export default App;
