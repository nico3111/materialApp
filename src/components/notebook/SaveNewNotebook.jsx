import React from 'react';

export default class SaveNewNotebook extends React.Component {
        constructor(props) {
            super(props);
            this.state = {
                serial_number: '',
                make: '',
                model: '',
                location: '',
                person: ''
            }
        }

        updateNewSerial_number(serial_number) {
            this.setState({ serial_number: serial_number});
        }

    async postData() {
        
        try {
            let result = await fetch('http://localhost:5000/Material/Notebook', {
                method: 'post',
                mode: 'no-cors',
                headers: {
                },
                body: JSON.stringify({
                    serial_number: this.state.serial_number,
                    make: 'test_make',
                    model: 'test_model'
                })
            })

            console.log('Result')
        } catch (error) {
            console.log(error)
        }
    }

    render() {
        // const setSerial_number = this.props.setSerial_number;
        // const setMake = this.props.setMake;
        // const setModel = this.props.setModel;
        // const setLocation = this.props.setLocation;
        // const setPerson = this.props.setPerson;

        return (
            <div>
                <input value={this.state.serial_number} onChange={(event) => this.updateNewSerial_number(event.target.value)} placeholder="Seriennummer"></input>
                <button onClick={() => this.postData()}>Hinzufügen</button>
            </div>
        )
    }
}