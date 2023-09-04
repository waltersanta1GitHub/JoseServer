// General Constants
const baseUrl = "http://localhost:8081";
const boton = document.getElementById("boton");

let testList = [];
let currentAnswerResult = [];
let selectedTest = {};
let username = "";
let succesCounter = 0;
let currentQuestionId = 0;
let currentTestId = 0;
// This bearer token require to be configured before accesst. this value is in JoseServer calling GENERATETOKEN() METHOD
const bearertoken= "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InRlc3R1c2VyIiwibmJmIjoxNjkzNTc0NjQ5LCJleHAiOjE2OTM1ODU0NDksImlhdCI6MTY5MzU3NDY0OX0.4r74A2n1zYUbNLWW4rg1hoJfmjyJtFga1EQBT8Py1uk";

const populateControlsMain =  () => {
  fetch(`${baseUrl}?alltestlist=*`,{   
    headers: {"Authorization":`Bearer ${bearertoken}`}
  })
    .then((response) => response.json())
    .then((data) => {
      var selectElement = document.getElementById("opcionMain");
      const options = data;
      testList = data;
      for (var i = 0; i < options.length; i++) {
        var option = document.createElement("option");
        option.value = options[i].testid;
        option.text = options[i].testname;
        selectElement.appendChild(option);
      }
    })
    .catch((error) => console.error("Error:", error));
};

boton.addEventListener("click", () => {
  const inputmessage = document.getElementById("errormessage");
  inputmessage.style.display = "none";

  const textBox = document.getElementById("usernametxt");
  if (textBox.value == "") {
    inputmessage.style.display = "inline";
  } else {
    username = textBox.value;
    disableControl("buttonnextid");
    changeScreen("seccion1", "seccion2");

    var selectElement = document.getElementById("opcionMain");
    let selectedTestId = parseInt(selectElement.value);
    currentTestId = selectedTestId - 1;
    selectedTest = testList[currentTestId];
    populateQuestion(selectedTest, currentQuestionId);
  }
});

const populateQuestion = (data, questionid) => {
  currentAnswerResult = [];
  var controlarea = document.getElementById("controlsareaid");
  disableControl("buttonnextid", true);
  const currentQuestions = data.questions;
  const question = currentQuestions[questionid];
  var questionText = document.getElementById("idquestiontext");
  questionText.innerText = `${question.questionid}-${question.questiontext}`;
  let newprogress = (data.questions.length - currentQuestionId) * 10;
  updateprogressbar(newprogress);

  for (var i = 0; i < question.options.length; i++) {
    var button = document.createElement("button");
    button.value = i;
    button.innerText = question.options[i];
    button.id = `${i}_button`;
    button.className = "buttonoption grid-item";
    button.addEventListener("click", mainClickHandler, false);
    controlarea.appendChild(button);
  }
};

const mainClickHandler = function (event) {
  const optionSelected = event.srcElement.innerText;
  addNewAnswer(optionSelected);
};

const addNewAnswer = (optionSelected) => {
  currentAnswerResult.push(optionSelected);
  disableControl("buttonnextid", false);
};

const validateAnswer = () => {
  if (currentAnswerResult.length > 0) {
    let answers =
      selectedTest.questions[currentAnswerResult[0].questionid].answers;
    if (answers.length == currentAnswerResult.length) {
      let checked = true;
      for (i = 0; i < currentAnswerResult.length; i++) {
        if (answers[i] === currentAnswerResult[i].answer) {
          checked = true;
        } else {
          checked = false;
        }
      }
      if (checked) {
        succesCounter++;
      }
    }
  }
};

const nextQuestion = () => {
  fetch(`${baseUrl}`, {
    method: "POST",
    body: JSON.stringify({
      "testid": selectedTest.testid,
      "questionid": currentQuestionId,
      "answercontent": currentAnswerResult,
    }),
    headers: {
      Authorization:`Bearer ${bearertoken}`,
      Accept: "application/json",
    },
  })
    .then((response) => response.json())
    .then((data) => {
      if (data) {
        succesCounter++;
        currentAnswerResult = [];
      }

      removeControls();
      if (currentQuestionId >= testList.length) {
        changeScreen("seccion2", "seccion3");
        var titleheader = document.getElementById("titleheaderid");
        var scoreheader = document.getElementById("scoreheaderid");
        titleheader.innerText = `Thank you, ${username}!`;
        scoreheader.innerText = `You have answered correctly on ${succesCounter} out of ${selectedTest.questions.length} questions`;
      } else {
        currentQuestionId++;
        populateQuestion(selectedTest, currentQuestionId);
      }
    })
    .catch((error) => console.error("Error in CheckAnswer:", error));
    
};

const updateprogressbar = (newvalue) => {
  var progressbar = document.getElementById("progressid");
  let previousproces = progressbar.value;
  progressbar.value = previousproces + newvalue;
};

const removeControls = () => {
  let element = document.getElementById("controlsareaid");
  while (element.firstChild) {
    element.removeChild(element.firstChild);
  }
};

const disableControl = (ctrlname, value) => {
  let element = document.getElementById(ctrlname);
  element.disabled = value;
};

const changeScreen = (previousscreen, nextscreen) => {
  const prevSection = document.getElementById(previousscreen);
  const nextSection = document.getElementById(nextscreen);
  prevSection.style.visibility = "hidden";
  nextSection.style.visibility = "visible";
};
