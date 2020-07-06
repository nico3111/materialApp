import React from 'react';
import '../notebook/AllNotebook.css'
import SaveNewNotebook from './SaveNewNotebook';



export default class ShowAllNotebook extends React.Component {

    state = {
        loading: true,
        allnotebooks: [],
    };


    async componentDidMount() {
        this.fetchNotebooks()
    }

    fetchNotebooks = async () => {
        const url = "http://192.168.0.94:8015/material/notebook";
        const response = await fetch(url);
        const data = await response.json();
        this.setState({ allnotebooks: data });
    }

    async deleteData(id) {

        if (window.confirm("Möchten Sie wirklich löschen")) {

            try {
                fetch("http://192.168.0.94:8015/material/notebook/" + id, {
                    method: 'delete',
                    mode: 'cors'
                }).then(this.fetchNotebooks)
    
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
                    <div className="line-text">Notebooks</div>
                    <div className="line2"></div>
                </div>

                {this.state.allnotebooks.map(allnotebooks => (
                    // <NotebookCard notebook={allnotebooks} />
                    <div className="notebooks">
                        <div className="show-list">
                            <div className="head-text">Notebook</div>
                            <div>Marke: {allnotebooks.make}</div><br></br>
                            <div>Modell: {allnotebooks.model}</div><br></br>
                            <div>Seriennummer: {allnotebooks.serial_number}</div><br></br>
                            <div>Person: {allnotebooks.person != null ? allnotebooks.person.name1 + " " + allnotebooks.person.name2 : ""}</div><br></br>
                            <div>Standort: {allnotebooks.classroom != null ? allnotebooks.classroom.addressloc.address.place : ""}</div><br></br>
                            <div>Räumlichkeit: {allnotebooks.classroom != null ? allnotebooks.classroom.room : ""}</div><br></br>

                            <div className="button-wrapper">
                                <div className="add-button2" onClick={() => this.deleteData(allnotebooks.id)}>Löschen</div>
                                <div className="add-button2" onClick={() => this.deleteData(allnotebooks.id)}>Update</div>
                            </div>

                        </div>
                    </div>))}
                <SaveNewNotebook fetchNotebooks={this.fetchNotebooks} />

            </div>
        )
    }
}
