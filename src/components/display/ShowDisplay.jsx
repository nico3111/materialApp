import React from 'react';
import '../notebook/AllNotebook.css'
import AddDisplay from './AddDisplay';

export default class ShowDisplay extends React.Component {

    state = {
        allDisplays: []
    };



    async componentDidMount() {
        this.fetchDisplays()
    }

    fetchDisplays = async () => {
        const url = "http://192.168.0.94:8015/material/display";
        const response = await fetch(url);
        const data = await response.json();
        this.setState({ allDisplays: data });
    }

    async deleteData(id) {

        if (window.confirm("Möchten Sie wirklich löschen")) {

            try {
                fetch("http://192.168.0.94:8015/material/display/" + id, {
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
                    <div className="line-text">Bildschirme</div>
                    <div className="line2"></div>
                </div>

                {this.state.allDisplays.map(allDisplays => (
                    <div className="notebooks">
                        <div className="show-list">
                            <div className="head-text">Bildschirm</div>
                            <div>Marke: {allDisplays.make}</div><br></br>
                            <div>Modell: {allDisplays.model}</div><br></br>
                            <div>Seriennummer: {allDisplays.serial_number}</div><br></br>
                            <div>Standort: {allDisplays.classroom != null ? allDisplays.classroom.addressloc.address.place : ""}</div><br></br>
                            <div>Räumlichkeit: {allDisplays.classroom != null ? allDisplays.classroom.room : ""}</div><br></br>
                            <div className="button-wrapper">
                                <div className="add-button2" onClick={() => this.deleteData(allDisplays.id)}>Löschen</div>
                                <div className="add-button2" onClick={() => this.deleteData(allDisplays.id)}>Update</div>
                            </div>
                        </div>
                    </div>))}
                <AddDisplay fetchDisplays={this.fetchDisplays} />
            </div>
        )
    }
}