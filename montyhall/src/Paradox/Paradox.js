import React, { Component } from 'react';
import './Paradox.css';
import axios from 'axios';

class Paradox extends Component {

    state = {
        noOfSimulations: 10,
        KeepDoorOption: 2,
        GameResultSummary : {
            totalWin: 0,
            totalLoose: 0
        },
        isLoading: false
    };

    onClickRunSimulation = () => {        
        console.log("Loading...");
        this.setState({ isLoading: true });
        axios.get("https://localhost:44363/api/v1/montyhall/simulate?noOfSimulations=" + this.state.noOfSimulations + "&KeepDoorOption=" + this.state.KeepDoorOption)
            .then(response => {   
                console.log("Success");
                this.setState({ isLoading: false });
                this.setState({ totalWin: response.data.totalWin })                
                this.setState({ totalLoose: response.data.totalLoose })                
            })
            .catch(err => {
                this.setState({ isLoading: false });
                console.log("Error occurred");
                console.log(err);                
            });
    }

    onChangeNoOfSimulationsHandler = (event) => {        
        this.setState({ noOfSimulations: event.target.value });        
    }

    onChangeKeepDoorOptionHandler = (event) => {        
        this.setState({ KeepDoorOption: event.target.value });
    }

    render() {
        return (
            <div className="Paradox">
                <h2>Monty Hall Paradox</h2>
                <label>Total Simulations:</label>
                <input type="number" id="txtTotalSimulations" defaultValue={this.state.noOfSimulations} onChange={this.onChangeNoOfSimulationsHandler} /><br/>

                <label>Keep Door Option:</label>
                <select value={this.state.KeepDoorOption} onChange={this.onChangeKeepDoorOptionHandler}>
                    <option value="1">Keep Door></option>
                    <option value="2">Do Not Keep Door></option>
                </select><br />

                <button onClick={this.onClickRunSimulation}>Run Simulation</button><br />
                <label>{this.state.isLoading && "Loading..."}</label>                
                <div>
                    <label>Win: {this.state.totalWin}</label><br />
                    <label>Loose: {this.state.totalLoose}</label>
                </div>                 
            </div>
            );
    }; 
}

export default Paradox;