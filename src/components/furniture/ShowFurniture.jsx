import React from 'react';
import '../notebook/AllNotebook.css'
import AddFurniture from './AddFurniture';

export default class ShowFurniture extends React.Component {

    state = {
        allFurniture: []
    };

    async componentDidMount() {
        this.fetchFurniture()
    }

    fetchFurniture = async () => {
        const url = "http://192.168.0.94:8015/material/furniture";
        const response = await fetch(url);
        const data = await response.json();
        this.setState({ allFurniture: data });
    }

    async deleteData(id) {

        if (window.confirm("Möchten Sie wirklich löschen")) {

            try {
                fetch("http://192.168.0.94:8015/material/furniture/" + id, {
                    method: 'delete',
                    mode: 'cors'
                }).then(this.fetchFurniture)
    
            } catch (error) {
                console.log(error)
            }
        }
    }

    render() {
        if (this.props.isShowing === false) {
            return null;
        }
        return (
            <div className="notebooks-wrapper">

                <div className="line-wrapper">
                    <div className="line"></div> 
                    <div className="line-text">Mobiliar</div>
                    <div className="line2"></div>
                </div>

                {this.state.allFurniture.map(allFurniture => (
                    <div className="notebooks">
                        <div className="show-list">
                            <div className="head-text">Mobiliar</div>
                            <div>Art: {allFurniture.type}</div><br></br>
                            <div>Anzahl: {allFurniture.quantity}</div><br></br>
                            <div>Standort: {allFurniture.classroom != null ? allFurniture.classroom.addressloc.address.place : ""}</div><br></br>
                            <div>Räumlichkeit: {allFurniture.classroom != null ? allFurniture.classroom.room : ""}</div><br></br>
                        
                            <div className="button-wrapper">
                                <div className="add-button2" onClick={() => this.deleteData(allFurniture.id)}>Löschen</div>
                                <div className="add-button2" onClick={() => this.deleteData(allFurniture.id)}>Update</div>
                            </div>
                        
                        </div>
                    </div>))}
                <AddFurniture fetchFurniture={this.fetchFurniture} />
            </div>
        )
    }
}