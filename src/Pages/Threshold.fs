module Pages.Threshold

open Feliz

/// The landing room. Deliberately austere: a single line, a short frame, one link.
/// No cards, no CTA buttons, no audio autoplay. See CLAUDE.md §6.1.

let render () =
    Html.main [
        prop.className "threshold"
        prop.children [
            Html.header [
                prop.className "threshold-plate"
                prop.children [
                    Html.p [
                        prop.className "threshold-eyebrow"
                        prop.lang "hy"
                        prop.text "Կոմիտաս Վարդապետ"
                    ]
                    Html.p [
                        prop.className "threshold-eyebrow-latin"
                        prop.text "Komitas Vardapet · 1869 – 1935"
                    ]
                ]
            ]

            Html.section [
                prop.className "threshold-thesis"
                prop.children [
                    Html.p [
                        prop.className "threshold-line hy"
                        prop.lang "hy"
                        prop.text "Ձայնը պահված։"
                    ]
                    Html.p [
                        prop.className "threshold-line en"
                        prop.lang "en"
                        prop.text "The voice preserved."
                    ]
                    Html.p [
                        prop.className "threshold-line ru"
                        prop.lang "ru"
                        prop.text "Голос сохранён."
                    ]
                ]
            ]

            Html.section [
                prop.className "threshold-frame"
                prop.children [
                    Html.p [
                        prop.text "A reading room around Komitas Vardapet — priest, ethnographer, composer. The man who wrote the folk voice down. Here you will find sheet music, recordings, and short readings, edited slowly, verified before published."
                    ]
                ]
            ]

            Html.footer [
                prop.className "threshold-nav"
                prop.children [
                    Html.a [ prop.href "/life"; prop.text "Life" ]
                    Html.span [ prop.className "sep"; prop.text "·" ]
                    Html.a [ prop.href "/songs"; prop.text "Songs" ]
                    Html.span [ prop.className "sep"; prop.text "·" ]
                    Html.a [ prop.href "/liturgy"; prop.text "Liturgy" ]
                    Html.span [ prop.className "sep"; prop.text "·" ]
                    Html.a [ prop.href "/circle"; prop.text "The Circle" ]
                    Html.span [ prop.className "sep"; prop.text "·" ]
                    Html.a [ prop.href "/silence"; prop.text "Silence" ]
                    Html.span [ prop.className "sep"; prop.text "·" ]
                    Html.a [ prop.href "/sources"; prop.text "Sources" ]
                ]
            ]

            Html.footer [
                prop.className "site-foot"
                prop.text "Komitas · The Reading Room · edited by Maggie Goshin · CC BY-NC 4.0 unless noted"
            ]
        ]
    ]
