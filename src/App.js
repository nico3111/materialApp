/**
 *
 * $$$$$$$\                                          $$\             $$\                                    $$\
 * $$  __$$\                                         $$ |            $$ |                                   $$ |
 * $$ |  $$ | $$$$$$\  $$\   $$\ $$$$$$$\   $$$$$$$\ $$$$$$$\        $$ |     $$\   $$\ $$$$$$$\   $$$$$$$\ $$$$$$$\
 * $$$$$$$\ |$$  __$$\ $$ |  $$ |$$  __$$\ $$  _____|$$  __$$\       $$ |     $$ |  $$ |$$  __$$\ $$  _____|$$  __$$\
 * $$  __$$\ $$ |  \__|$$ |  $$ |$$ |  $$ |$$ /      $$ |  $$ |      $$ |     $$ |  $$ |$$ |  $$ |$$ /      $$ |  $$ |
 * $$ |  $$ |$$ |      $$ |  $$ |$$ |  $$ |$$ |      $$ |  $$ |      $$ |     $$ |  $$ |$$ |  $$ |$$ |      $$ |  $$ |
 * $$$$$$$  |$$ |      \$$$$$$  |$$ |  $$ |\$$$$$$$\ $$ |  $$ |      $$$$$$$$\\$$$$$$  |$$ |  $$ |\$$$$$$$\ $$ |  $$ |
 * \_______/ \__|       \______/ \__|  \__| \_______|\__|  \__|      \________|\______/ \__|  \__| \_______|\__|  \__|
 *
 *
 *
 *                                     $$\      $$\            $$$$$$\  $$\
 *                                     $$$\    $$$ |          $$  __$$\ \__|
 *                                     $$$$\  $$$$ | $$$$$$\  $$ /  \__|$$\  $$$$$$\
 *                                     $$\$$\$$ $$ | \____$$\ $$$$\     $$ | \____$$\
 *                                     $$ \$$$  $$ | $$$$$$$ |$$  _|    $$ | $$$$$$$ |
 *                                     $$ |\$  /$$ |$$  __$$ |$$ |      $$ |$$  __$$ |
 *                                     $$ | \_/ $$ |\$$$$$$$ |$$ |      $$ |\$$$$$$$ |
 *                                     \__|     \__| \_______|\__|      \__| \_______|
 *
 *
 * Version 2.0
 * Author: BrunchFamily
 * copyright BrunchLunchMafia
 * */


import React from 'react';
import './App.css';
import Head from './components/header/Head';
import ShowNotebook from './components/notebook/ShowNotebooks';
import FilterButton from './components/filterButton/FilterButton';
import ShowDispaly from './components/display/ShowDisplay';
import ShowFurniture from './components/furniture/ShowFurniture';
import ShowBook from './components/books/ShowBook';
import ShowEquipment from './components/equipment/ShowEquipment';
import ShowSearch from './components/search/ShowSearch';

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
      showSearch: false,
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
            <FilterButton onClick={() => this.setState({ showSearch: !this.state.showSearch })} title="Suchen" isActive={this.state.showSearch} />
          </div>
          {this.state.showAllNotebooks && <ShowNotebook isShowing={this.state.showAllNotebooks} />}
          {this.state.showAllDisplay && <ShowDispaly isShowing={this.state.showAllDisplay} />}
          {this.state.showAllFurniture && <ShowFurniture isShowing={this.state.showAllFurniture} />}
          {this.state.showAllBooks && <ShowBook isShowing={this.state.showAllBooks} />}
          {this.state.showEquipment && <ShowEquipment isShowing={this.state.showEquipment} />}
          {this.state.showSearch && <ShowSearch isShowing={this.state.showSearch} />}
        </div>

      </div>
    );
  }
}

export default App;
