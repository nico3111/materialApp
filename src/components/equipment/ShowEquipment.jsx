import React from 'react';
import '../notebook/AllNotebook.css'
import AddEquipment from './AddEquipment';

export default class ShowEquipment extends React.Component {

    state = {
        allEquipment: []
    };



    async componentDidMount() {
        this.fetchDisplays()
    }

    fetchDisplays = async () => {
        const url = "http://192.168.0.94:8015/material/equipment";
        const response = await fetch(url);
        const data = await response.json();
        this.setState({ allEquipment: data });
        console.log(data)
    }

    async deleteData(id) {

        if (window.confirm("Möchten Sie wirklich löschen")) {

            try {
                fetch("http://192.168.0.94:8015/material/equipment/" + id, {
                    method: 'delete',
                    mode: 'cors'
                }).then(this.fetchDisplays)
    
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
                    <div className="line-text">Zubehör</div>
                    <div className="line2"></div>
                </div>

                {this.state.allEquipment.map(allEquipment => (
                    <div className="notebooks">
                        <div className="show-list">
                            <div className="head-text">Zubehör</div>
                            <div>Art: {allEquipment.type}</div><br></br>
                            <div>Modell: {allEquipment.model}</div><br></br>
                            <div>Marke: {allEquipment.make}</div><br></br>
                            <div>Menge: {allEquipment.quantity}</div><br></br>
                            <div>Standort: {allEquipment.classroom != null ? allEquipment.classroom.addressloc.address.place : ""}</div><br></br>
                            <div>Räumlichkeit: {allEquipment.classroom != null ? allEquipment.classroom.room : ""}</div><br></br>
                        
                            <div className="button-wrapper">
                                <div className="add-button2" onClick={() => this.deleteData(allEquipment.id)}>Löschen</div>
                                <div className="add-button2" onClick={() => this.deleteData(allEquipment.id)}>Update</div>
                            </div>
                        
                        </div>
                    </div>))}
                <AddEquipment fetchDisplays={this.fetchDisplays} />
            </div>
        )
    }
}