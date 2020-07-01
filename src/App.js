import React from 'react';
import './App.css';
import Head from './components/header/Head';
import ShowAllNotebook from './components/notebook/ShowAllNotebooks';
import FilterButton from './components/filterButton/FilterButton';
import SaveNewNotebook from './components/notebook/SaveNewNotebook';

class App extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      id: '',
      showAllNotebooks: false,
      showAllDesks: false
    }
  }

  setId = (id) => {
    this.setState({ id: id });
  }

  render() {
    return (
      <div>
        <Head />
        {/* <input value="" onChange={(event) => this.setId(event.target.value)} />
        {this.state.id !== '' && <ShowNotebook id={this.state.id} />} */}
        <div className="body">
          <div className="filterbar">
          <FilterButton onClick={() => this.setState({ showAllNotebooks: !this.state.showAllNotebooks })} title="Notebooks" isActive={this.state.showAllNotebooks} />
            <FilterButton onClick={() => this.setState({ showAllDesks: !this.state.showAllDesks })} title="Tische" />
          </div>
          {this.state.showAllNotebooks && <ShowAllNotebook isShowing={this.state.showAllNotebooks} />}
        </div>
        <SaveNewNotebook/>
      </div>
    );
  }
}

export default App;
