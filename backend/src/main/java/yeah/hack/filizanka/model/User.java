package yeah.hack.filizanka.model;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.CollectionTable;
import javax.persistence.Column;
import javax.persistence.ElementCollection;
import javax.persistence.Entity;
import javax.persistence.FetchType;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.JoinTable;
import javax.persistence.ManyToMany;
import javax.persistence.ManyToOne;
import javax.persistence.OneToMany;
import java.util.Set;

@Entity
@AllArgsConstructor
@NoArgsConstructor
@Data
@JsonIgnoreProperties(ignoreUnknown = true)
@Builder
public class User {

    @Id
    @Column(name="user_id")
    private Long userId;

    private String name;

    private Long credits;

    @ManyToOne(optional = true)
    @JoinColumn(name = "skin_id")
    private Skin activeSkin;

    @ManyToMany(fetch = FetchType.LAZY)
    @JoinTable(name = "user_skin",
            joinColumns = @JoinColumn(name = "user_id"),
            inverseJoinColumns = @JoinColumn(name = "skin_id"))
    private Set<Skin> skins;

    @ManyToMany(fetch = FetchType.LAZY)
    @JoinTable(name = "user_carriage",
            joinColumns = @JoinColumn(name = "user_id"),
            inverseJoinColumns = @JoinColumn(name = "carriage_id"))
    private Set<Carriage> carriages;

    @ElementCollection(fetch = FetchType.LAZY)
    @CollectionTable(
            name = "user_eggs",
            joinColumns = @JoinColumn(name = "user_id", referencedColumnName = "user_id")
    )
    private Set<Egg> eggs;

    @ManyToOne(optional = true)
    private TrainRide currentTrainRide;

    @ManyToOne(optional = true)
    @JoinColumn(name = "destination_id")
    private Point destination;

    @ManyToOne(optional = true)
    @JoinColumn(name = "last_point_id")
    private Point lastPoint;

}
