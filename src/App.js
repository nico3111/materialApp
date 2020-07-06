import React from 'react';
import './App.css';
import Head from './components/header/Head';
import ShowAllNotebook from './components/notebook/ShowAllNotebooks';
import FilterButton from './components/filterButton/FilterButton';
import SaveNewNotebook from './components/notebook/SaveNewNotebook';
import ShowDispaly from './components/display/ShowDisplay';
import AddDisplay from './components/display/AddDisplay';
import ShowFurniture from './components/furniture/ShowFurniture';
import AddFurniture from './components/furniture/AddFurniture';
import ShowBook from './components/books/ShowBook';
import ShowEquipment from './components/equipment/ShowEquipment';

class App extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      id: '',
      showAllNotebooks: false,
      showAllDesks: false,
      showAllDisplay: false,
      showAllFurniture: false,
      showAllBooks: false,
      showEquipment: false,
    }
  }

  setId = (id) => {
    this.setState({ id: id });
  }

  render() {
    return (
      <div>
        <Head />
        <div className="body">
          <div className="filterbar">
            <FilterButton onClick={() => this.setState({ showAllNotebooks: !this.state.showAllNotebooks })} title="Notebooks" isActive={this.state.showAllNotebooks} />
            <FilterButton onClick={() => this.setState({ showAllDisplay: !this.state.showAllDisplay })} title="Bildschirme" isActive={this.state.showAllDisplay} />
            <FilterButton onClick={() => this.setState({ showAllFurniture: !this.state.showAllFurniture })} title="Mobiliar" isActive={this.state.showAllFurniture} />
            <FilterButton onClick={() => this.setState({ showAllBooks: !this.state.showAllBooks })} title="Bücher" isActive={this.state.showAllBooks} />
            <FilterButton onClick={() => this.setState({ showEquipment: !this.state.showEquipment })} title="Zubehör" isActive={this.state.showEquipment} />
          </div>
          {this.state.showAllNotebooks && <ShowAllNotebook isShowing={this.state.showAllNotebooks} />}
          {this.state.showAllDisplay && <ShowDispaly isShowing={this.state.showAllDisplay} />}
          {this.state.showAllFurniture && <ShowFurniture isShowing={this.state.showAllFurniture} />}
          {this.state.showAllBooks && <ShowBook isShowing={this.state.showAllBooks} />}
          {this.state.showEquipment && <ShowEquipment isShowing={this.state.showEquipment} />}
        </div>

      </div>
    );
  }
}

export default App;
