'------------------------------------------------------------------------------
' <summary>
'	Base class intended to be used to contain information respective of every AM technology and material combination.
'	Acting as a template, designated as an abstract class with child classes for each combination.
' </summary>
' <author>Jasper Koid</author>
' <created>24-AUG-2025</created>
'------------------------------------------------------------------------------

Imports System.Collections.Generic
Imports System.Linq

Public MustInherit Class AMProfile
	' Material and technology properties vary depending on child class.
	' BaseWeights adjusts based on material shared among each child, therefore Weights differ among child class.
	Public Property Material As String
	Public Property Technology As String
	Protected Weights As Dictionary(Of String, Double)
	Private ReadOnly BaseWeights As Dictionary(Of String, Double)

	'Mapping categorical inputs into numerical values of the criteria using dictionaries.
	Protected MustOverride ReadOnly Property CategoryMappings As Dictionary(Of String, Dictionary(Of String, Double))


	' Methods of interpreting numeric inputs, outputting a value between 1 and 10.
	Protected MustOverride Function InterpretPartComplexity(rawComplexity As Double) As Double
	Protected MustOverride Function InterpretOverhangComplexity(rawOverhang As Double) As Double
    Protected MustOverride Function InterpretImpossibleFeatures(rawImpossibleFeatures As Boolean) As Double

	' Each child class will implement their own multipliers for each part purpose - basic adaption is based on material type.
	Protected MustOverride Function GetPurposeMultipliers(partPurpose As String) As Dictionary(Of String, Double)

	'Construct default weightings for each AM profile (as each profile depending on part purpose may shift their respective weights).
	Public Sub New()
		BaseWeights = New Dictionary(Of String, Double) From {
			{"Precision", 0.069}, {"LeadTime", 0.129}, {"PostProcessing", 0.152}, {"Volume", 0.15},
			{"Complexity", 0.241}, {"ImpossibleFeatures", 0.236}, {"Overhang", 0.024}
			}
		Weights = New Dictionary(Of String, Double)(BaseWeights)
	End Sub

	'A public sub to adjust the weights, as the protected function should only be called from the child class.
	Public Sub AdjustWeightsForPurpose(partPurpose As String)
		Dim multipliers = GetPurposeMultipliers(partPurpose)

		'Apply multipliers.
		For Each key In BaseWeights.Keys
			If multipliers.ContainsKey(key) Then
				Weights(key) = BaseWeights(key) * multipliers(key)
			Else
				Weights(key) = BaseWeights(key)
			End If
		Next

		' Normalise the weights.
		Dim sum As Double = Weights.Values.Sum()
		If sum > 0 Then
			Dim keys = Weights.Keys.ToList()
			For Each key In keys
				Weights(key) = Weights(key) / sum
			Next
		End If
	End Sub


	' Calculatescore function sums all values multiplied by their respective weights.
	Public Function CalculateScore(criteria As ManufacturingCriteria) As Double
		' Interpret categorical inputs using the profile-specific mappings.
		Dim precisionValue As Double = CategoryMappings("Precision")(criteria.CategoricalInputs("Precision"))
		Dim leadTimeValue As Double = CategoryMappings("LeadTime")(criteria.CategoricalInputs("LeadTime"))
		Dim postProcessingValue As Double = CategoryMappings("PostProcessing")(criteria.CategoricalInputs("PostProcessing"))
		Dim volumeValue As Double = CategoryMappings("Volume")(criteria.CategoricalInputs("Volume"))

		' Interpret numeric inputs using the profile-specific functions.
		Dim interpretedComplexity As Double = InterpretPartComplexity(criteria.NumericInputs("Complexity"))
		Dim interpretedOverhang As Double = InterpretOverhangComplexity(criteria.NumericInputs("Overhang"))
		Dim impossibleFeatureValue As Double = InterpretImpossibleFeatures(criteria.HasNonMachinableFeatures)

		' Start the calculating procedure!
		Dim score As Double = 0
		score += precisionValue * Weights("Precision")
		score += leadTimeValue * Weights("LeadTime")
		score += postProcessingValue * Weights("PostProcessing")
		score += volumeValue * Weights("Volume")
		score += interpretedComplexity * Weights("Complexity")
		score += interpretedOverhang * Weights("Overhang")
		score += impossibleFeatureValue * Weights("ImpossibleFeatures")
		score *= 100 ' Normalised values multiplied by 100 to scale.
		Return Math.Round(Math.Max(0, Math.Min(score, 100))) 'Ideally should constrain values between 0 and 100 with no trailing 0s for a clean UI.
	End Function

	Public ReadOnly Property GetWeights As Dictionary(Of String, Double)
		Get
			Return Weights
		End Get
	End Property

	Public ReadOnly Property GetCategoryMappings As Dictionary(Of String, Dictionary(Of String, Double))
		Get
			Return CategoryMappings
		End Get
	End Property
End Class

'------------------------------------------------------------------------------
' <summary>
'	Designated helper class to aggregate user inputs and geometrical data  to be passed to the CalculateScore function.
' </summary>
' <author>Jasper Koid</author>
' <created>24-AUG-2025</created>
'------------------------------------------------------------------------------
Public Class ManufacturingCriteria
	Public Property CategoricalInputs As Dictionary(Of String, String)
	Public Property NumericInputs As Dictionary(Of String, Double)
	Public Property HasNonMachinableFeatures As Boolean
	Public Sub New(categorical As Dictionary(Of String, String),
				   numeric As Dictionary(Of String, Double),
					nonMachinableFeatures As Boolean)
		Me.CategoricalInputs = categorical
		Me.NumericInputs = numeric
		Me.HasNonMachinableFeatures = nonMachinableFeatures
	End Sub
End Class