module App

open Browser.Dom
open Fable.Core.JsInterop

// React 18 client entry via Feliz-style interop.
// We call ReactDOM.createRoot directly to keep the bootstrap tiny.

let root : obj = importMember "react-dom/client"
let container = document.getElementById "root"
let createRoot : obj -> obj = importMember "react-dom/client"

let reactRoot = createRoot container
reactRoot?render (Pages.Threshold.render ())
