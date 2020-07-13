import React from 'react';
import '../notebook/AllNotebook.css'
import AddNotebook from './AddNotebook';
import NotebookModal from './NotebookModal';
const { fetchPersons } = require('../../util/HttpHelper')

export default class ShowNotebook extends React.Component {

    state = {
        loading: true,
        allnotebooks: [],
        open: false,
        showUpdate: false,
        toUpdate: null,
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

    onOpenModal = (toUpdate) => {
        this.setState({
            open: true,
            toUpdate: toUpdate,
            personToUpdate: toUpdate.person
        });
    }
    onCloseModal = () => {
        this.setState({
            open: false,
            toUpdate: null
        });
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
        const { open } = this.state;

        return (

            <div className="notebooks-wrapper">

                <div className="line-wrapper">
                    <div className="line"></div>
                    <div className="line-text">Notebooks</div>
                    <div className="line2"></div>
                </div>
                <AddNotebook fetchNotebooks={this.fetchNotebooks} />
                {this.state.allnotebooks.map(allnotebook => {

                    return (
                        <div className="notebooks">
                            <div className="show-list">
                                <div className="head-text">Notebook</div>
                                <div>Marke: {allnotebook.make}</div><br></br>
                                <div>Modell: {allnotebook.model}</div><br></br>
                                <div>SN: {allnotebook.serial_number}</div><br></br>
                                <div>Person: {allnotebook.person != null ? allnotebook.person.name1 + " " + allnotebook.person.name2 : ""}</div><br></br>
                                <div>Standort: {allnotebook.classroom && allnotebook.classroom.addressloc != null ? allnotebook.classroom.addressloc.address.place : ""}</div><br></br>
                                <div>Räumlichkeit: {allnotebook.classroom != null ? allnotebook.classroom.room : ""}</div><br></br>

                                <div className="button-wrapper">
                                    <div className="add-button2" onClick={() => this.deleteData(allnotebook.id)}>Löschen</div>
                                    <div className="add-button2" onClick={() => this.onOpenModal(allnotebook)}>Ändern</div>
                                </div>

                                {this.state.toUpdate != null && <NotebookModal fetchNotebooks={this.fetchNotebooks} toUpdate={this.state.toUpdate} personToUpdate={this.state.toUpdate.person} open={open} onClose={this.onCloseModal} center showCloseIcon={false}>
                                </NotebookModal>}

                            </div>
                        </div>
                    )
                }
                )}
            </div>
        )
    }
}
